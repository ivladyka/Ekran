<%@ WebHandler Language="C#" Class="Galery" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;

public class Galery : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        List<GalleryClass> sbGalery = new List<GalleryClass>();
        string h = "";
        string w = "";
        sbGalery.Add(AddGalery("1", "http://localhost:15149/WWW/Images/1.jpg", w, h));
        sbGalery.Add(AddGalery("2", "http://localhost:15149/WWW/Images/2.jpg", w, h));
        sbGalery.Add(AddGalery("3", "http://localhost:15149/WWW/Images/3.jpg", w, h));
        sbGalery.Add(AddGalery("4", "http://localhost:15149/WWW/Images/5.jpg", w, h));
        sbGalery.Add(AddGalery("5", "http://localhost:15149/WWW/Images/6.jpg", w, h));
        //sbGalery.Add(AddGalery("2", "3"));

        /*sbGalery.Add(AddGalery("24879", "http://img.gallerama.com/users/alekbiotic/24879_9._DSC3435.jpg", "979", "650"));
        sbGalery.Add(AddGalery("34661", "http://img.gallerama.com/users/alekbiotic/34661_9._DSC6988.jpg", "979", "650"));*/
        context.Response.Write(new JavaScriptSerializer().Serialize(sbGalery));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private GalleryClass AddGalery(string id, string src, string w, string h)
    {
        GalleryClass g = new GalleryClass();
        g.id = id;
        g.src = src;
        g.w = w;
        g.h = h;
        return g;
    }
}

public class GalleryClass
{
    string _src;
    string _id;
    string _w;
    string _h;

    public string id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string src
    {
        get { return _src; }
        set { _src = value; }
    }

    public string w
    {
        get { return _w; }
        set { _w = value; }
    }
    
    public string h
    {
        get { return _h; }
        set { _h = value; }
    }
}