using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PHPServiceServer
{
    public class Config
    {
        XmlDocument _xmlDoc;

        public Config(string strFile)
        {
            Load(strFile);
        }

        private void Load(string strFile)
        {
            XmlElement theBook = null, theElem = null, root = null;
            _xmlDoc = new XmlDocument();
            try
            {
                _xmlDoc.Load(strFile);
                
            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// string game_db_host = f_Read("Configs", "Server", "game_db_host");
        /// </summary>
        /// <param name="strRootNode"></param>
        /// <param name="strNode"></param>
        /// <param name="strChildNode"></param>
        /// <returns></returns>
        public string f_Read(string strRootNode, string strNode, string strChildNode)
        {
            XmlNode xn = _xmlDoc.SelectSingleNode(strRootNode);
            XmlNode xnl = xn.SelectSingleNode(strNode);
            XmlNode ChildNode = xnl.SelectSingleNode(strChildNode);

            //XmlElement tXmlElement  = (XmlElement)xnl;
            //string strIP = tXmlElement.GetAttribute("IP").ToString();         //<Server IP="192.168.0.227" port="8000" HttpPort="8080">

            
            //string game_db_host = tXmlAttributeCollection.
            return ChildNode.InnerText;
        }

    }
}
