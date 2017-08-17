using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoMake.Models
{
    public class Task
    {
        #region FiledsPlace    
        private int id_person;
        public int Person_id
        {
            get { return id_person; }
            set { id_person = value; }
        }

        private int category;
        public int Category
        {
            get { return category; }
            set { category = value; }
        }

        private int service;
        public int Service
        {
            get { return service; }
            set { service = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;
        public string Description
        {
            get { return description;  }
            set { description = value; }
        }

        private int price;
        public int Price
        {
            get { return price;  }
            set { price = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone;  }
            set { phone = value; }
        }

        private string date_begin;
        public string Date_Begin
        {
            get { return date_begin;  }
            set { date_begin = value; }
        }

        private string date_end;
        public string Date_End
        {
            get { return date_end; }
            set { date_end = value; }
        }

        private string date_exeq_begin;
        public string Date__Exeq_Begin
        {
            get { return date_exeq_begin; }
            set { date_exeq_begin = value; }
        }

        private string time_exeq_begin;
        public string Time_Exeq_Begin
        {
            get { return time_exeq_begin; }
            set { time_exeq_begin = value; }
        }

        private string date_exeq_end;
        public string Date_Exeq_End
        {
            get { return date_exeq_end; }
            set { date_exeq_end = value; }
        }

        private string time_exeq_end;
        public string Time_Exeq_End
        {
            get { return time_exeq_end; }
            set { time_exeq_end = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string name_person;
        public string Name_Person
        {
            get { return name_person; }
            set { name_person = value; }
        }

        private string secname_person;
        public string Secname_Person
        {
            get { return secname_person; }
            set { secname_person = value; }
        }

        private string creation;
        public string Creation
        {
            get { return creation; }
            set { creation = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private int views;
        public int Views
        {
            get { return views; }
            set { views = value; }
        }

        private int money;
        public int Money
        {
            get { return money; }
            set { money = value; }
        }
        #endregion FiledsPlace

        #region Methods



        #endregion Methods

    }
}