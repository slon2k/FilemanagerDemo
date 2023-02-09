using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilemanagerDemo;
public interface IFileHandler
{
    public void HandleFile(string source, string destination);
}
