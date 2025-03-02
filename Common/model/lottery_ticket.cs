﻿using Maticsoft.Common.Util;
using System;
namespace Maticsoft.Common.model
{
	/// <summary>
	/// lottery_ticket:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class lottery_ticket
	{
        public lottery_ticket()
        {
        }
		public lottery_ticket(lotteryTicket lt)
        {
            this.bet_code = lt.betCode;
            this.bet_num = lt.betNum;
            this.bet_price = lt.betPrice;
            this.cancel_money = lt.cancelMoney;
            this.com_port = lt.comPort;
            this.create_date = lt.createDate;
            this.exc_handling_record = lt.excHandlingRecord;
            this.exception_description = lt.exceptionDescription;
            this.issue = lt.issue;
            this.license_id = lt.licenseId;
            this.multiple = lt.multiple;
            this.order_id = lt.orderId;
            this.order_odds = lt.orderOdds;
            this.order_rqs = lt.orderRqs;
            this.play_type = lt.playType;
            this.sent_num = lt.sentNum;
            this.stop_time = lt.stopTime;
            this.storeid = lt.storeid;
            this.terminal_number = lt.terminalNumber;
            this.ticket_date = lt.ticketDate;
            this.ticket_id = lt.ticketId;
            this.ticket_odds = lt.ticketOdds;
            this.ticket_rqs = lt.ticketRqs;
            this.ticket_state = lt.ticketState;
            this.userid = lt.userid;
            this.username = lt.username;
            this.zj_flag = lt.zj_flag;
            this.err_ticket_sign = lt.err_ticket_sign;
            this.betCode_info = lt.betCode_info;
            this.ticket_info = lt.ticket_info;
            this.is_feedback = lt.is_feedback;
            
            this.return_pass_type = lt.return_pass_type;
            this.return_license_id = lt.return_license_id;
            this.return_license_name = lt.return_license_name;
            this.return_issue = lt.return_issue;
            this.return_issue_num = lt.return_issue_num;
            this.return_play_name = lt.return_play_name;
            this.return_multiple = lt.return_multiple;
            this.return_money = lt.return_money;
            this.return_bet_info = lt.return_bet_info;
        }
		#region Model
		private Int64 _ticket_id;
        private Int64 _order_id;
        private Int64 _userid;
		private string _username;
        private Int64 _storeid;
        private Int64 _license_id;
		private string _play_type;
        private string _create_date = DateUtil.getServerDateTime(DateUtil.DATE_FMT_STR1);
		private string _ticket_date;
		private string _bet_code;
		private string _bet_num;
		private string _multiple;
		private string _bet_price;
		private string _stop_time;
		private string _ticket_state;
		private string _issue;
		private string _order_odds;
		private string _ticket_odds="";
		private string _order_rqs;
		private string _ticket_rqs;
		private string _terminal_number;
		private string _exc_handling_record;
		private string _com_port;
		private string _sent_num;
		private string _exception_description;
		private string _cancel_money="0.00";
        private string _zj_flag = "0";
        private Int64 _err_ticket_sign = 0;
        private string _ticket_info;//出票信息——票花信息
        private Int64 _is_feedback;

        private string _return_pass_type="0";
        private Int64 _return_license_id=0;
        private string _return_license_name="0";
        private string _return_issue ="0";
        private Int64 _return_issue_num = 0;
        private Int64 _return_play_id = 0;
        private string _return_play_name="0";
        private Int64 _return_multiple =0;
        private Int64 _return_money =0;
        private string _return_bet_info="0";

        public string return_pass_type
        {
            get { return _return_pass_type; }
            set { _return_pass_type = value; }
        }
        public Int64 return_license_id
        {
            get { return _return_license_id; }
            set { _return_license_id = value; }
        }
		/// <summary>
		/// 
		/// </summary>
        public Int64 ticket_id
		{
			set{ _ticket_id=value;}
			get{return _ticket_id;}
		}
		/// <summary>
		/// 
		/// </summary>
        public Int64 order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
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
		public string create_date
		{
			set{ _create_date=value;}
			get{return _create_date;}
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
		public string bet_code
		{
			set{ _bet_code=value;}
			get{return _bet_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bet_num
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
		public string stop_time
		{
			set{ _stop_time=value;}
			get{return _stop_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ticket_state
		{
			set{ _ticket_state=value;}
			get{return _ticket_state;}
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
		public string order_odds
		{
			set{ _order_odds=value;}
			get{return _order_odds;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ticket_odds
		{
			set{ _ticket_odds=value;}
			get{return _ticket_odds;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string order_rqs
		{
			set{ _order_rqs=value;}
			get{return _order_rqs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ticket_rqs
		{
			set{ _ticket_rqs=value;}
			get{return _ticket_rqs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string terminal_number
		{
			set{ _terminal_number=value;}
			get{return _terminal_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string exc_handling_record
		{
			set{ _exc_handling_record=value;}
			get{return _exc_handling_record;}
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
		public string sent_num
		{
			set{ _sent_num=value;}
			get{return _sent_num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string exception_description
		{
			set{ _exception_description=value;}
			get{return _exception_description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cancel_money
		{
			set{ _cancel_money=value;}
			get{return _cancel_money;}
		}

        private String _betCode_info;

        public String betCode_info
        {
            get { return this._bet_code; }
            set { _betCode_info = value; }
        }

        public Int64 is_feedback
        {
            get { return _is_feedback; }
            set { _is_feedback = value; }
        }

        public string return_license_name
        {
            get { return _return_license_name; }
            set { _return_license_name = value; }
        }

        public string return_issue
        {
            get { return _return_issue; }
            set { _return_issue = value; }
        }

        public Int64 return_issue_num
        {
            get { return _return_issue_num; }
            set { _return_issue_num = value; }
        }

        public string return_play_name
        {
            get { return _return_play_name; }
            set { _return_play_name = value; }
        }

        public Int64 return_multiple
        {
            get { return _return_multiple; }
            set { _return_multiple = value; }
        }

        public Int64 return_money
        {
            get { return _return_money; }
            set { _return_money = value; }
        }

        public string return_bet_info
        {
            get { return _return_bet_info; }
            set { _return_bet_info = value; }
        }

        public string ticket_info
        {
            get { return _ticket_info; }
            set { _ticket_info = value; }
        }

        public Int64 err_ticket_sign
        {
            get { return _err_ticket_sign; }
            set { _err_ticket_sign = value; }
        }

        public string zj_flag
        {
            get { return _zj_flag; }
            set { _zj_flag = value; }
        }

        public long return_play_id
        {
            get{return _return_play_id;}
            set{_return_play_id = value;}
        }
        #endregion Model

        public lottery_ticket copy()
        {
            lottery_ticket newthis = new lottery_ticket();
            newthis.bet_code = this.bet_code;
            newthis.bet_num = this.bet_num;
            newthis.bet_price = this.bet_price;
            newthis.cancel_money = this.cancel_money;
            newthis.com_port = this.com_port;
            newthis.create_date = this.create_date;
            newthis.exc_handling_record = this.exc_handling_record;
            newthis.exception_description = this.exception_description;
            newthis.issue = this.issue;
            newthis.license_id = this.license_id;
            newthis.multiple = this.multiple;
            newthis.order_id = this.order_id;
            newthis.order_odds = this.order_odds;
            newthis.order_rqs = this.order_rqs;
            newthis.play_type = this.play_type;
            newthis.sent_num = this.sent_num;
            newthis.stop_time = this.stop_time;
            newthis.storeid = this.storeid;
            newthis.terminal_number = this.terminal_number;
            newthis.ticket_date = this.ticket_date;
            newthis.ticket_id = this.ticket_id;
            newthis.ticket_odds = this.ticket_odds;
            newthis.ticket_rqs = this.ticket_rqs;
            newthis.ticket_state = this.ticket_state;
            newthis.userid = this.userid;
            newthis.username = this.username;
            newthis.zj_flag = this.zj_flag;
            newthis.err_ticket_sign = this.err_ticket_sign;
            newthis.betCode_info = this.betCode_info;
            newthis.ticket_info = this.ticket_info;
            newthis.is_feedback = this.is_feedback;

            newthis.return_pass_type = this.return_pass_type;
            newthis.return_license_id = this.return_license_id;
            newthis.return_license_name = this.return_license_name;
            newthis.return_issue = this.return_issue;
            newthis.return_issue_num = this.return_issue_num;
            newthis.return_play_name = this.return_play_name;
            newthis.return_multiple = this.return_multiple;
            newthis.return_money = this.return_money;
            newthis.return_bet_info = this.return_bet_info;

            return newthis;
        }
    }
}

