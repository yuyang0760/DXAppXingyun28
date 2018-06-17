// Pceggs.tools.XmlControl
using System.IO;
using System.Xml;

internal class XmlControl
{
    public XmlDocument doc = null;

    public XmlElement root = null;

    public void LoadXmlString(string xmlString)
    {
        doc = new XmlDocument();
        doc.LoadXml(xmlString);
        root = doc.DocumentElement;
    }

    public void LoadXmlFile(FileInfo xmlFile)
    {
        if (!(xmlFile?.Exists ?? false))
        {
            throw new FileNotFoundException($"要解析的文件不存在{xmlFile.FullName}。");
        }
        doc = new XmlDocument();
        doc.Load(xmlFile.FullName);
        root = doc.DocumentElement;
    }

    public XmlNode SelectSingleNode(string xPath)
    {
        return root.SelectSingleNode(xPath);
        
    }

    public XmlNodeList SelectNodes(string xPath)
    {
        return root.SelectNodes(xPath);
    }

    public void Save(string filePath)
    {
        doc.Save(filePath);
    }
}
