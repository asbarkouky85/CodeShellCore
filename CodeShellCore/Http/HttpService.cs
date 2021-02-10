using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using Newtonsoft.Json;
using CodeShellCore.Files;
using CodeShellCore.Text;
using System.Text;
using CodeShellCore.Helpers;
using CodeShellCore.Services;

namespace CodeShellCore.Http
{
    public abstract class HttpService : ServiceBase, IHttpService
    {
        HttpClient Client;

        protected abstract string BaseUrl { get; }
        protected virtual object AppendToQuery { get; set; }

        private Dictionary<string, string> _headers;
        public Dictionary<string, string> Headers
        {
            get
            {
                if (_headers == null)
                    _headers = new Dictionary<string, string>();
                return _headers;
            }
        }

        public string LogToFile { get; set; }
        private static bool _servicePointSet = false;

        public static HttpService GetInstance(string baseUrl = null)
        {
            return new DefaultHttpService(baseUrl);
        }
        public HttpService()
        {
            if (!_servicePointSet)
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                ServicePointManager.Expect100Continue = true;
                _servicePointSet = true;
            }

        }

        #region Loggers

        public void AppendLog(Exception ex, string message = "Local Server Error")
        {
            if (LogToFile == null)
                return;

            List<string> lst = new List<string>();

            HttpResult res = new HttpResult
            {
                Code = 1,
                Message = message
            };

            res.SetException(ex);
            lst.Add(" ");
            lst.Add("--------------------------------------------------------------------");
            lst.Add(" ");
            lst.Add(res.Message);
            lst.Add(res.ExceptionMessage);
            lst.Add(" ");
            lst.Add("--------------------------------------------------------------------");
            lst.Add(" ");

            lst.AddRange(res.StackTrace);

            File.AppendAllLines(LogToFile, lst);
        }

        public void AppendLog(string url, HttpStatusCode code, string responseBody, object sent = null)
        {
            if (LogToFile == null)
                return;

            List<string> lst = new List<string>();
            lst.Add(" ");
            lst.Add("--------------------------------------------------------------------");
            lst.Add(" ");
            lst.Add("Request : " + url);

            if (sent != null)
            {
                lst.Add(JsonConvert.SerializeObject(sent, Formatting.Indented));
            }

            lst.Add("Response Status : " + code.ToString() + " " + ((int)code));
            lst.Add("Response Message :");
            lst.Add(responseBody);
            lst.Add(" ");
            lst.Add("--------------------------------------------------------------------");
            lst.Add(" ");

            File.AppendAllLines(LogToFile, lst);
        }

        public void AppendLog(string url, IEnumerable<FileData> data, HttpStatusCode code, string responseBody)
        {
            if (LogToFile == null)
                return;

            List<string> lst = new List<string>();
            lst.Add(" ");
            lst.Add("--------------------------------------------------------------------");
            lst.Add(" ");
            lst.Add("Request : " + url);

            foreach (var up in data)
                lst.Add("File Path : " + up.FullPath);

            lst.Add("Response Status : " + code.ToString() + " " + ((int)code));
            lst.Add("Response Message :");
            lst.Add(responseBody);
            lst.Add(" ");
            lst.Add("--------------------------------------------------------------------");
            lst.Add(" ");

            File.AppendAllLines(LogToFile, lst);
        }

        public void AppendException(string url, Exception ex, object sent = null)
        {
            if (LogToFile == null)
                return;

            List<string> lst = new List<string>();
            lst.Add(" ");
            lst.Add("--------------------------------------------------------------------");
            lst.Add(" ");
            lst.Add("Request : " + url);
            if (sent != null)
            {
                lst.Add(JsonConvert.SerializeObject(sent, Formatting.Indented));
            }
            lst.AddRange(GetExceptionLog(ex));
            lst.Add(" ");
            lst.Add("--------------------------------------------------------------------");
            lst.Add(" ");
            File.AppendAllLines(LogToFile, lst);
        }

        private List<string> GetExceptionLog(Exception ex)
        {
            List<string> lst = new List<string>();
            lst.Add(ex.GetType().FullName);
            lst.Add(ex.Message);
            lst.Add(ex.StackTrace);

            if (ex.InnerException != null)
            {
                lst.AddRange(GetExceptionLog(ex.InnerException));
            }
            return lst;
        }

        #endregion


        protected Uri AppendQuery(Uri uri, object obj)
        {

            PropertyInfo[] infs = obj.GetType().GetProperties();
            string qm = string.IsNullOrEmpty(uri.Query) ? "?" : "&";
            List<string> data = new List<string>();
            foreach (PropertyInfo inf in infs)
            {
                var val = inf.GetValue(obj);
                if (val != null)
                    data.Add(inf.Name + "=" + val.ToString());
            }
            string ur = uri.AbsoluteUri + qm + string.Join("&", data);
            return new Uri(ur);
        }

        protected virtual void SetSecurityProtocol() { }

        protected virtual Uri GetUri(string url, object query = null)
        {
            Uri uri = new Uri(Utils.CombineUrl(BaseUrl, url));

            if (query != null)
                uri = AppendQuery(uri, query);
            if (AppendToQuery != null)
                uri = AppendQuery(uri, AppendToQuery);

            if (Headers.Count > 0)
            {
                foreach (var h in Headers)
                    Client.DefaultRequestHeaders.TryAddWithoutValidation(h.Key, h.Value);
            }
            SetSecurityProtocol();
            return uri;
        }

        public HttpResponseMessage Post<T>(string url, T data, object query = null) where T : class
        {
            Task<HttpResponseMessage> tsk = PostAsync(url, data, query);
            Task.WaitAll(tsk);
            if (!tsk.Result.IsSuccessStatusCode)
                throw new CodeShellHttpException(tsk.Result);
            return tsk.Result;
        }

        public T PostAs<T>(string url, object data, object query = null) where T : class
        {
            Task<HttpResponseMessage> tsk = PostAsync(url, data, query);
            Task.WaitAll(tsk);
            if (!tsk.Result.IsSuccessStatusCode)
                throw new CodeShellHttpException(tsk.Result);
            else {
                var tt = tsk.Result.Content.ReadAsStringAsync();
                Task.WaitAll(tt);
                return tt.Result.FromJson<T>();
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T data, object query = null) where T : class
        {
            Client = new HttpClient();

            Uri uri = GetUri(url, query);

            try
            {
                var st = data.ToJson();
                HttpResponseMessage mes = await Client.PostAsync(uri, new StringContent(st, Encoding.UTF8, "application/json"));
                if (LogToFile != null)
                {
                    string res = await mes.Content.ReadAsStringAsync();
                    AppendLog(uri.AbsoluteUri, mes.StatusCode, res, data);
                }
                return new HttpResponseMessage()
                {
                    RequestMessage = mes.RequestMessage,
                    StatusCode = mes.StatusCode,
                    Content = mes.Content
                };
            }
            catch (Exception ex)
            {
                if (LogToFile != null)
                {
                    AppendException(uri.AbsoluteUri, ex, data);
                }
                return ExceptionToResponse(ex);
            }
        }

        public HttpResponseMessage Get(string url, object query = null)
        {
            Task<HttpResponseMessage> tsk = GetAsync(url, query);
            tsk.Wait(2000);
            if (!tsk.Result.IsSuccessStatusCode)
                throw new CodeShellHttpException(tsk.Result);
            return tsk.Result;
        }

        public async Task<T> GetAsyncAs<T>(string url, object query = null) where T : class
        {
            var res = await GetAsync(url, query);
            if (res.IsSuccessStatusCode)
            {
                string data = await res.Content.ReadAsStringAsync();
                if (data.TryRead(out T result))
                    return result;
                else
                    throw new CodeShellHttpException(HttpStatusCode.NotImplemented, "Unable to parse object to type " + typeof(T).Name);
            }
            else
            {
                throw new CodeShellHttpException(res);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string url, object query = null)
        {
            Client = new HttpClient();

            Uri uri = GetUri(url, query);

            try
            {

                HttpResponseMessage mes = await Client.GetAsync(uri);

                if (LogToFile != null)
                {
                    string res = await mes.Content.ReadAsStringAsync();
                    AppendLog(uri.AbsoluteUri, mes.StatusCode, res);
                }
                return new HttpResponseMessage()
                {
                    RequestMessage = mes.RequestMessage,
                    StatusCode = mes.StatusCode,
                    Content = mes.Content
                };
            }
            catch (Exception ex)
            {
                if (LogToFile != null)
                {
                    AppendException(uri.AbsoluteUri, ex);
                }
                return ExceptionToResponse(ex);
            }

        }

        public async Task<FileBytes> DownloadFileAsync(string url, object query = null)
        {
            HttpResponseMessage mes = await GetAsync(url, query);
            if (mes.IsSuccessStatusCode)
            {
                FileBytes b = new FileBytes();
                b.Bytes = await mes.Content.ReadAsByteArrayAsync();

                b.FileName = url.GetAfterLast("/")?.GetBeforeFirst("?");

                if (mes.Content.Headers.ContentDisposition != null)
                    b.FileName = mes.Content.Headers.ContentDisposition.FileName;

                if (mes.Content.Headers.ContentType != null)
                    b.MimeType = mes.Content.Headers.ContentType.MediaType;
                else
                    b.MimeType = MimeData.GetFileMimeType(b.FileName);

                return b;
            }
            else
            {
                string message = await mes.Content.ReadAsStringAsync();
                throw new CodeShellHttpException(mes.StatusCode, message);
            }
        }

        public FileBytes DownloadFile(string url, object query = null)
        {
            try
            {
                url = url.Replace("\\", "/");
                Task<FileBytes> obj = Task.Run(() => DownloadFileAsync(url, query));
                Task.WaitAll(obj);
                return obj.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<HttpResponseMessage> UploadFiles(string url, IEnumerable<FileData> files, object query = null)
        {
            Client = new HttpClient();
            Uri uri = GetUri(url, query);

            try
            {
                List<MemoryStream> strs = new List<MemoryStream>();
                using (var content = new MultipartFormDataContent())
                {
                    foreach (FileData fil in files)
                    {

                        byte[] byts = File.ReadAllBytes(fil.FullPath);
                        MemoryStream str = new MemoryStream(byts);
                        strs.Add(str);
                        StreamContent cont = new StreamContent(str);
                        cont.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "\"" + fil.FieldName + "\"",
                            FileName = "\"" + fil.FileName + "\""
                        };
                        content.Add(cont, fil.FieldName, fil.FileName);
                    }
                    HttpResponseMessage mes = await Client.PostAsync(uri, content);

                    mes.EnsureSuccessStatusCode();
                    string res = await mes.Content.ReadAsStringAsync();
                    if (LogToFile != null)
                        AppendLog(uri.AbsoluteUri, files, mes.StatusCode, res);
                    return new HttpResponseMessage { StatusCode = mes.StatusCode, Content = mes.Content };
                }
            }
            catch (Exception ex)
            {
                if (LogToFile != null)
                {
                    AppendException(uri.AbsoluteUri, ex);
                }
                return ExceptionToResponse(ex);
            }

        }

        protected HttpResponseMessage ExceptionToResponse(Exception ex)
        {
            HttpResponseMessage mes = new HttpResponseMessage(HttpStatusCode.ExpectationFailed)
            {
                Content = new StringContent(ex.Message)
            };

            return mes;
        }
    }
}
