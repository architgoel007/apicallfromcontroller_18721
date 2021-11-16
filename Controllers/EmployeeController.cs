using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using apicallfromcontroller_18721.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace apicallfromcontroller_18721.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Create(int Id = 0)
        {
            ViewBag.BT = "Save";
            Emp _emp = new Emp();
            if (Id > 0)
            {
                HttpClient _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:44332/api/");
                HttpResponseMessage consumeapi = _client.GetAsync("EmpCrud?Id=" + Id.ToString()).Result;
                if (consumeapi.IsSuccessStatusCode)
                {
                    var content = consumeapi.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<dynamic>(content);
                    _emp.ID = Convert.ToInt32(data[0]["Id"]);
                    _emp.Name = data[0]["Name"];
                    _emp.Position = data[0]["Position"];
                    _emp.Age = Convert.ToInt32(data[0]["Age"]);
                    _emp.Salary = Convert.ToInt32(data[0]["Salary"]);
                    ViewBag.BT = "Update";
                }

            }
            return View(_emp);
        }




        [HttpPost]
        public ActionResult Create(Emp _emp)
        {
            if (_emp.ID > 0)
            {
                HttpClient _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:44332/api/EmpCrud");
                string data = JsonConvert.SerializeObject(_emp);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                _client.PutAsync(_client.BaseAddress, content);
            }
            else
            {
                HttpClient _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:44332/api/EmpCrud");
                string data = JsonConvert.SerializeObject(_emp);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                _client.PostAsync(_client.BaseAddress, content);
            }
            return View();
        }

        public ActionResult Display()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44332/api/EmpCrud");
            List<Emp> modelList = new List<Emp>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var jsonResult = JsonConvert.DeserializeObject(content).ToString();
                modelList = JsonConvert.DeserializeObject<List<Emp>>(jsonResult);
            }
            return View(modelList);
        }

        public ActionResult Delete(int Id = 0)
        {
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44332/api/EmpCrud");
            var delrecord = _client.DeleteAsync("EmpCrud/" + Id.ToString());
            delrecord.Wait();
            var displaydata = delrecord.Result;
            if (displaydata.IsSuccessStatusCode)
            {
                return RedirectToAction("Display");
            }
            return RedirectToAction("Display");
        }


    }
}