﻿using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// lottery_order:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class lottery_order
	{
		public lottery_order()
		{}
		#region Model
		private Int64 _id;
        private Int64 _userid;
		private string _username;
        private Int64 _storeid;
        private Int64 _license_id;
		private string _play_type;
		private string _pass_type;
		private string _order_date;
		private string _single_flag;
		private string _bet_code;
        private Int64 _bet_num;
		private string _multiple;
		private string _bet_price;
		private string _bet_state;
		private string _issue;
        private Int64 _err_ticket_sign = 0;
		private string _sch_info;
		private string _mult_info;
		private string _bet_from;
		private string _bet_type;
		private string _ticket_oper;
        private Int64 _canceled_num;
        private Int64 _canceled_money;
        private Int64 _errticket_num;
		private string _ticket_date;
		private string _del_date;
        private Int64 _ticket_money = 0;
		private string _com_port;
        private Int64 _ticket_num;
        private Int64 _total_money;
        private Int64 _total_tickets_num;
        private Int64 _is_in_feedback_form;        
        private Int64 _is_in_print_form;
        private Int64 _is_in_error_form;
        private string _stop_time;

        public string stop_time
        {
            get { return _stop_time; }
            set { _stop_time = value; }
        }
             
		/// <summary>
		/// 
		/// </summary>
        public Int64 id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 storeid
		{
			set{ _storeid=value;}
			get{return _storeid;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 license_id
		{
			set{ _license_id=value;}
			get{return _license_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string play_type
		{
			set{ _play_type=value;}
			get{return _play_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pass_type
		{
			set{ _pass_type=value;}
			get{return _pass_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string order_date
		{
			set{ _order_date=value;}
			get{return _order_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string single_flag
		{
			set{ _single_flag=value;}
			get{return _single_flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bet_code
		{
			set{ _bet_code=value;}
			get{return _bet_code;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 bet_num
		{
			set{ _bet_num=value;}
			get{return _bet_num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string multiple
		{
			set{ _multiple=value;}
			get{return _multiple;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bet_price
		{
			set{ _bet_price=value;}
			get{return _bet_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bet_state
		{
			set{ _bet_state=value;}
			get{return _bet_state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string issue
		{
			set{ _issue=value;}
			get{return _issue;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 err_ticket_sign
		{
            set { _err_ticket_sign = value; }
            get { return _err_ticket_sign; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string sch_info
		{
			set{ _sch_info=value;}
			get{return _sch_info;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mult_info
		{
			set{ _mult_info=value;}
			get{return _mult_info;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bet_from
		{
			set{ _bet_from=value;}
			get{return _bet_from;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bet_type
		{
			set{ _bet_type=value;}
			get{return _bet_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ticket_oper
		{
			set{ _ticket_oper=value;}
			get{return _ticket_oper;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 canceled_num
		{
			set{ _canceled_num=value;}
			get{return _canceled_num;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 errticket_num
		{
			set{ _errticket_num=value;}
			get{return _errticket_num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ticket_date
		{
			set{ _ticket_date=value;}
			get{return _ticket_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string del_date
		{
			set{ _del_date=value;}
			get{return _del_date;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 ticket_money
		{
			set{ _ticket_money=value;}
			get{return _ticket_money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string com_port
		{
			set{ _com_port=value;}
			get{return _com_port;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 ticket_num
		{
			set{ _ticket_num=value;}
			get{return _ticket_num;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 total_money
		{
			set{ _total_money=value;}
			get{return _total_money;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 total_tickets_num
		{
			set{ _total_tickets_num=value;}
			get{return _total_tickets_num;}
		}

        public Int64 is_in_feedback_form
        {
            get { return _is_in_feedback_form; }
            set { _is_in_feedback_form = value; }
        }

        public Int64 is_in_print_form
        {
            get { return _is_in_print_form; }
            set { _is_in_print_form = value; }
        }

        public Int64 is_in_error_form
        {
            get { return _is_in_error_form; }
            set { _is_in_error_form = value; }
        }

        
        public Int64 canceled_money
        {
            get { return _canceled_money; }
            set { _canceled_money = value; }
        }
		#endregion Model

	}
}

