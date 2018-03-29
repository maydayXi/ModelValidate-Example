using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjModelValidate.Models;                  // 使用資料模型

namespace prjModelValidate.Controllers
{
    public class HomeController : Controller
    {
        // 資料庫物件
        dbStudentEntities db = new dbStudentEntities();

        /// <summary>
        /// 顯示所有學生資料
        /// </summary>
        /// <returns> 首頁 </returns>
        // GET: Home
        public ActionResult Index()
        {
            // 將 tStudent 資料表轉成 List 物件
            var students = db.tStudent.ToList();
            // 並回傳至 View 
            return View(students);
        }

        /// <summary>
        /// 新增學生資料
        /// </summary>
        /// <returns> 新增資料頁面 </returns>
        // GET： Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 新增學生資料
        /// </summary>
        /// <param name="student"> 學生資料表單物件 </param>
        /// <returns> 首頁 / 新增頁面 </returns>
        // POST： Create
        // 按下 Submit 鈕時，會將表單欄位對應到 tStudent 物件屬性
        // 若通過資料驗證，則進行新增學生記錄
        [HttpPost]
        public ActionResult Create(tStudent student)
        {
            // 如果通過驗證
            if (ModelState.IsValid)
            {
                db.tStudent.Add(student);           // 新增學生資料
                db.SaveChanges();                   // 存儲變更
                return RedirectToAction("Index");   // 重新導向 Index 頁面
            }

            // 未通過驗證導向 Create 頁面
            // 並回傳未通過的學生資料
            return View(student);
        }

        /// <summary>
        /// 刪除學生資料
        /// </summary>
        /// <param name="id"> 學號 </param>
        /// <returns> 首頁 </returns>
        // GET： Delete
        public ActionResult Delete(string id)
        {
            // 找出符合學號的學生資料
            var student = db.tStudent
                .Where(m => m.fStuId == id)
                .FirstOrDefault();

            db.tStudent.Remove(student);        // 刪除該筆學生資料
            db.SaveChanges();                   // 存儲變更
            return RedirectToAction("Index");   // 重新導首頁
        }
    }
}