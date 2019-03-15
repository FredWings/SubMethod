using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Public
{
    public class XmlException : Exception
    {
        public XmlException(int errNo, string errInfo)
        {
            this.ErrNo = errNo;
            this.ErrInfo = errInfo;
        }

        public int ErrNo { get; set; }

        public string ErrInfo { get; set; }

        public override string ToString() 
            => "errNo: " + this.ErrNo.ToString() + ", errInfo: " + ErrInfo;         
    }

    public class XmlReaderException : XmlException
    {
        public XmlReaderException(int errNo, string errInfo) : base(errNo, errInfo) { }
    }

    public class XmlWriterException : XmlException
    {
        public XmlWriterException(int errNo, string errInfo) : base(errNo, errInfo) { }
    }
}
