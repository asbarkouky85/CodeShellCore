using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Text;


namespace CodeShellCore.Services
{
    public enum ThumbSize { Sm = 256, Xs = 96 }
    public class ImagesService : ServiceBase
    {
        public void ReplaceColor(string path, Color oldColor, Color newColor)
        {

            Bitmap lockedBitmap = new Bitmap(Bitmap.FromFile(path));


            for (int y = 0; y < lockedBitmap.Height; y++)
            {
                for (int x = 0; x < lockedBitmap.Width; x++)
                {
                    var p = lockedBitmap.GetPixel(x, y);
                    var e = p.ToArgb();
                    if (p == oldColor)
                    {
                        lockedBitmap.SetPixel(x, y, newColor);
                    }
                }
            }
            lockedBitmap.Save(path.Replace(".jpg", ".png"), ImageFormat.Png);

        }
        public Bitmap ResizeImage(Image current, int maxWidth, int maxHeight)
        {

            int width, height;
            if (current.Width > current.Height)
            {
                width = maxWidth;
                height = Convert.ToInt32(current.Height * maxHeight / (double)current.Width);
            }
            else
            {
                width = Convert.ToInt32(current.Width * maxWidth / (double)current.Height);
                height = maxHeight;
            }

            var canvas = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(current, 0, 0, width, height);
            }
            return canvas;
        }

        public Bitmap CropImage(Image im, CropPixels rect)
        {
            Bitmap image = new Bitmap((int)rect.W, (int)rect.H);
            using (Graphics gr = Graphics.FromImage(image))
            {
                gr.CompositingQuality = CompositingQuality.HighSpeed;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.CompositingMode = CompositingMode.SourceCopy;
                float start = rect.X < 0 ? 0 : rect.X;

                gr.FillRectangle(Brushes.White, 0, 0, image.Width, image.Height);
                gr.DrawImage(im,
                     new RectangleF(0, 0, image.Width, image.Height),
                     new RectangleF(start, rect.Y, rect.W - start, rect.H),
                     GraphicsUnit.Pixel
                     );

            }

            return image;
        }



        public string StoreThumb(string filePath, ThumbSize size, string extension = null)
        {
            int maxWidth = (int)size;
            int maxHeight = (int)size;
            string fileName = FileUtils.GetThumbPath(filePath, size, extension);
            if (!File.Exists(filePath))
                return fileName;

            FileInfo inf = new FileInfo(filePath);

            string newPath = Path.Combine(inf.Directory.FullName, fileName);
            Utils.CreateFolderForFile(newPath);

            if (File.Exists(newPath))
                return fileName;

            Image current = Image.FromFile(filePath);
            Bitmap canvas = ResizeImage(current, maxWidth, maxHeight);

            canvas.Save(newPath);
            return FileUtils.GetThumbUrl(filePath, size);
        }

        public string StoreThumb(string filePath, CropPixels rect, ThumbSize size, string extension = null)
        {
            int maxWidth = (int)size;
            int maxHeight = (int)size;

            FileInfo inf = new FileInfo(filePath);
            string fileName = FileUtils.GetThumbPath(filePath, size, extension);
            string newPath = Path.Combine(inf.Directory.FullName, fileName);
            Utils.CreateFolderForFile(newPath);

            Bitmap image = new Bitmap(maxWidth, maxHeight);
            Image im = Image.FromFile(filePath);
            Bitmap cropped = CropImage(im, rect);

            float ratio = image.Width / rect.W;
            using (Graphics gr = Graphics.FromImage(image))
            {

                gr.FillRectangle(Brushes.White, 0, 0, maxWidth, maxHeight);

                float start = rect.X < 0 ? (rect.X * -1 * ratio) : 0;

                gr.CompositingQuality = CompositingQuality.HighSpeed;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.CompositingMode = CompositingMode.SourceCopy;
                gr.DrawImage(cropped, start, 0, image.Width, image.Height);

            }
            if (File.Exists(newPath))
                File.Delete(newPath);
            image.Save(newPath, ImageFormat.Png);
            string th = FileUtils.GetThumbUrl(filePath, size);
            return th;
        }

        public FileBytes GetThumb(string filePath, int maxWidth, int maxHeight)
        {
            if (!File.Exists(filePath))
                return null;

            Image current = Image.FromFile(filePath);
            Bitmap canvas = ResizeImage(current, maxWidth, maxHeight);

            var bytes = new byte[0];


            using (MemoryStream st = new MemoryStream())
            {
                canvas.Save(st, current.RawFormat);
                bytes = st.ToArray();
            }

            FileInfo inf = new FileInfo(filePath);
            var FileName = inf.Name.GetBeforeLast(".") + "__" + maxWidth + "x" + maxHeight + inf.Extension;
            FileBytes b = new FileBytes(FileName, bytes);
            return b;
        }
    }
}
