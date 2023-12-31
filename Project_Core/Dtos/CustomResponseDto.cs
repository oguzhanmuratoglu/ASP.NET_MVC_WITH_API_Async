﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project_Core.Dtos
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, Errors = null };
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errrors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errrors };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string errror)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { errror } };
        }
    }
}
