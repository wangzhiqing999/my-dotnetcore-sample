using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFramework.Util
{

    /// <summary>
    /// 整数类型的时间处理类.
    /// 
    /// 单位精确到分钟。
    /// 例如 
    /// 9:00  存储为  900
    /// 18：30  存储为  1830
    /// </summary>
    public class IntTimerProcesser
    {

        /// <summary>
        /// 数值是否是一个有效的时间数值.
        /// </summary>
        /// <param name="timeValue"></param>
        public static bool IsActiveTime(int timeValue) {

            if(timeValue < 0) {
                // 时间数值不能小于零
                return false;
            }

            if (timeValue % 10000 >= 2400)
            {
                // 时间数值不能大于等于 2400
                return false;
            }

            if (timeValue % 100 >= 60)
            {
                // 数值的后两位， 不能大于等于60.
                return false;
            }

            // 如果能执行到这里，那么认为时间数值是有效的.
            return true;
        }


        /// <summary>
        /// 针对指定的时间， 增加分钟数.
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int IntTimeAddMinutes(int timeValue, int minutes)
        {
            // 日期.
            int days = timeValue / 10000;

            // 时间.
            int hourMins = timeValue % 10000;


            // 以今天零点为基准.增加 初始时间的  时 与 分.
            DateTime dt = DateTime.Today.AddHours(hourMins / 100).AddMinutes(hourMins % 100);

            // 增加 参数 指定的分钟数.
            dt = dt.AddMinutes(minutes);

            // 预期结果.
            int result = dt.Hour * 100 + dt.Minute;

            if (minutes > 0 && result < hourMins)
            {
                // 结果小于  开始的时间.
                // 那么认为 跨日了.
                result += 10000;
            }

            if (days > 0)
            {
                result += 10000 * days;
            }


            // 返回结果.
            return result;
        }


        /// <summary>
        /// 返回用于显示的时间格式.
        /// </summary>
        /// <param name="timeValue"></param>
        /// <returns></returns>
        public static string GetDisplayTimeString(int timeValue)
        {
            if (timeValue >= 10000)
            {
                return String.Format("次日{0:00:00}", timeValue % 10000);
            }
            return String.Format("{0:00:00}", timeValue);
        }




        /// <summary>
        /// 返回用于显示的时间区间格式.
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static string GetDisplayTimeRangeString(int timeValue, int minutes)
        {
            int finishTime = IntTimeAddMinutes(timeValue, minutes);

            if (timeValue >= 10000)
            {
                return String.Format("次日{0:00:00}～{1:00:00}", timeValue % 10000, finishTime % 10000);
            }

            return String.Format(
                "{0:00:00}～{1}",
                timeValue, GetDisplayTimeString(finishTime));
        }





        /// <summary>
        /// 返回 两个 时间相减的分钟数值.
        /// </summary>
        /// <param name="timeValueFinish"></param>
        /// <param name="timeValueStart"></param>
        /// <returns></returns>
        public static int Subtraction(int timeValueFinish, int timeValueStart)
        {
            // 小时.
            int hour = timeValueFinish / 100 - timeValueStart / 100;
            
            // 分钟.
            int minute = timeValueFinish % 100 - timeValueStart % 100;


            return hour * 60 + minute;
        }


    }

}
