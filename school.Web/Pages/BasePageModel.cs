﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages;

public class BasePageModel : PageModel
{
    public string ErrorMessage { get; set; } = "";

    protected int GetSchoolId()
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        return int.TryParse(sId, out int schoolId) ? schoolId : -1;
    }

    protected IActionResult RedirectToSchoolList()
    {
        return RedirectToPage("/Schools/List", "error");
    }
}
