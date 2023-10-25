﻿using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{                                                   //dtolar için de yapılabilir.
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);//fiyat 0dan büyük olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//categori idsi1 olanın fiyatı 10dan büyük olmalı
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("ürünler a ile başlamalı.");//startwitha method



        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}