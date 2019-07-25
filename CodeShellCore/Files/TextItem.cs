using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodeShellCore.Files
{
     class TextItem
    {
        public string Text { get; set; }
        public AsyncFileHandler LFile { get; set; }

        public TextItem(string txt,AsyncFileHandler fi) 
        {
            Text = txt;
            LFile = fi;
        }

        public void AppendFunction() 
        {
            LFile.Append(Text);
        }
    }
}
