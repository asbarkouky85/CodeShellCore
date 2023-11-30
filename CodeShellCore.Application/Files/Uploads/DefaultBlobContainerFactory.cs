using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Files.Uploads
{
    public class DefaultBlobContainerFactory : IBlobContainerFactory
    {
        private readonly IOptions<FileUploadOptions> options;

        public DefaultBlobContainerFactory(IOptions<FileUploadOptions> options)
        {
            this.options = options;
        }
        public IBlobContainer GetContainer(string name)
        {
            BlobContainerConfiguration config = null;
            if (!string.IsNullOrEmpty(name))
            {
                var c = options.Value.Containers?.FirstOrDefault(e => e.Name == name);
                if (c != null)
                    config = c;
            }
            config = config ?? options.Value.Default;
            return new FileSystemBlobContainer(config);
        }
    }
}
