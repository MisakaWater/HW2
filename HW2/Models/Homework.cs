using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HW2.Models
{
    public class Homework
    {
        public Homework()
        {
            AnswerFiles = new List<UploadFile>();
        }

        public long Id { get; set; }
        public string GUID { get; set; }
        [Display(Name = "课程")]
        public string Course { get; set; }
        [Display(Name ="作业标题")]
        public string WorkTitle { get; set; }
        [Display(Name = "作业内容")]
        public string WorkContent { get; set; }
        [Display(Name = "作业答案文本")]
        public string AnswerText { get; set; }
        [Display(Name = "作业答案文件")]
        public List<UploadFile> AnswerFiles { get; set; }

        [Display(Name ="发布时间")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name="结束时间")]
        public DateTime EndDate { get; set; }
    }

}
