﻿using System;
using System.Collections.Generic;
using System.Text;

using MyMiniTradingSystem.Model;

namespace MyMiniTradingSystem.Service
{
    public interface ITradableCommodityService
    {
        /// <summary>
        /// 插入商品信息
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="resultMessage"></param>
        /// <returns></returns>
        bool CreateTradableCommodity(TradableCommodity newData, ref string resultMessage);


        /// <summary>
        /// 获取所有的商品信息.
        /// </summary>
        /// <returns></returns>
        List<TradableCommodity> GetAll();


        /// <summary>
        /// 获取特定的商品信息.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        TradableCommodity Get(string code);


    }
}
