using System;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    /// <summary>
    /// Представляет реализацию Unix-времени.
    /// </summary>
    public class UnixTime : DataSerializer, ICloneable
    {
        /// <summary>
        /// Возвращает текущее время в формате Unix.
        /// </summary>
        /// <returns></returns>
        public static Int32 GetUnixTime()
        {
            return ToUnixTime(DateTime.Now);
        }

        /// <summary>
        /// Преобразует DateTime в Unix таймстемп.
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns></returns>
        public static Int32 ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        /// <summary>
        /// Преобразует Unix таймстемп в DateTime.
        /// </summary>
        /// <param name="timeStamp">Таймстемп</param>
        /// <returns></returns>
        public static DateTime ToDateTime(Int32 timeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timeStamp);
        }

        /// <summary>
        /// Текущие дата/время
        /// </summary>
        public static UnixTime Now
        {
            get
            {
                return new UnixTime();
            }
        }

        /// <summary>
        /// Таймстемп.
        /// </summary>
        public int Timestamp;

        /// <summary>
        /// Дата/время
        /// </summary>
        public DateTime Time
        {
            get
            {
                return ToDateTime(Timestamp);
            }
            set
            {
                Timestamp = ToUnixTime(value);
            }
        }

        /// <summary>
        /// Базовая инициализация. Время присваивается текущее.
        /// </summary>
        public UnixTime()
        {
            Time = DateTime.Now;
        }

        /// <summary>
        /// Инициализация объекта.
        /// </summary>
        /// <param name="timeStamp">Unix таймстемп</param>
        public UnixTime(int timeStamp)
        {
            Time = ToDateTime(timeStamp);
        }

        /// <summary>
        /// Инициализация объекта.
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        public UnixTime(DateTime dateTime)
        {
            Time = dateTime;
        }

        public override string ToString()
        {
            return String.Format("{0}", Time, Timestamp);
        }

        public static UnixTime operator +(UnixTime time1, UnixTime time2)
        {
            return new UnixTime(time1.Timestamp + time2.Timestamp);
        }

        public static UnixTime operator +(UnixTime time, int timeStamp)
        {
            return new UnixTime(time.Timestamp + timeStamp);
        }

        public static UnixTime operator +(UnixTime time1, DateTime time2)
        {
            return new UnixTime(time1.Timestamp + ToUnixTime(time2));
        }

        public static UnixTime operator -(UnixTime time1, UnixTime time2)
        {
            return new UnixTime(time1.Timestamp - time2.Timestamp);
        }

        public static UnixTime operator -(UnixTime time, int timeStamp)
        {
            return new UnixTime(time.Timestamp - timeStamp);
        }

        public static UnixTime operator -(UnixTime time1, DateTime time2)
        {
            return new UnixTime(time1.Timestamp - ToUnixTime(time2));
        }
        
        /// <summary>
        /// Неявное преобразование (cast) DateTime в UnixTime.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static implicit operator UnixTime(DateTime dateTime)
        {
            return new UnixTime(dateTime);
        }

        /// <summary>
        /// Неявное преобразование (cast) UnixTime в DateTime.
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static implicit operator DateTime(UnixTime unixTime)
        {
            return unixTime.Time;
        }

        public object Clone()
        {
            return new UnixTime(Timestamp);
        }

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Timestamp);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Timestamp = ds.ReadInt32();
            return base.Deserialize(ds);
        }
    }
}
