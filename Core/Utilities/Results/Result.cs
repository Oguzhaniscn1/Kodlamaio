using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success,string message):this(success)
        {
            Message = message;
            
        }
        //ileri seviye ctor yapısı -- kapsayıcı bir yapıya ulaştık, 
        public Result(bool success)
        {
            Success = success;
        }



        //get olayını set ediyyoruz bunu const sayesinde yapabiliyoruz get olayı readonly bir olaydır.
        public bool Success { get; }

        public string Message { get; }
    }
}
