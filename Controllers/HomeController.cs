using MVCAdoNet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAdoNet.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection cnn = null;
        // GET: Home
        public ActionResult Index()
        {
            cnn = new SqlConnection(ConfigurationManager.AppSettings.Get("cnn"));
            cnn.Open();
            SqlCommand cmd = new SqlCommand("select * from Contacts", cnn);
            SqlDataReader dr = cmd.ExecuteReader();

            List<Contact> glContacts = new List<Contact>();

            while (dr.Read())
            {
                Contact c = new Contact
                {
                    ContactId = Int32.Parse(dr["ContactId"].ToString()),
                    ContactName = dr["ContactName"].ToString(),
                    Location = dr["Location"].ToString()
                };

                glContacts.Add(c);
            }
            cnn.Close();
            return View(glContacts);
        }

        public ActionResult Create()
        {
           return View();
        }
        [HttpPost]
        public ActionResult Create(string str)
        {
            using (cnn = new SqlConnection(ConfigurationManager.AppSettings.Get("cnn")))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into Contacts values (@ContactName, @Location)", cnn);
                cmd.Parameters.AddWithValue("@ContactName", Request.Form["name"]);
                cmd.Parameters.AddWithValue("@Location", Request.Form["location"]);

                cmd.ExecuteNonQuery();
            }
        return RedirectToAction("Index");
        }
        public ActionResult CreateHelper()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateHelper(Contact c)
        {
            using (cnn = new SqlConnection(ConfigurationManager.AppSettings.Get("cnn")))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into Contacts values (@ContactName, @Location)", cnn);
                //cmd.Parameters.AddWithValue("@ContactName", Request.Form["name"]);
                //cmd.Parameters.AddWithValue("@Location", Request.Form["location"]);

                cmd.Parameters.AddWithValue("@ContactName", c.ContactName);
                cmd.Parameters.AddWithValue("@Location", c.Location);
                 
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}