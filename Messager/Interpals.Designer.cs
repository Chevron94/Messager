﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace Interpals
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class InterpalsEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new InterpalsEntities object using the connection string found in the 'InterpalsEntities' section of the application configuration file.
        /// </summary>
        public InterpalsEntities() : base("name=InterpalsEntities", "InterpalsEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new InterpalsEntities object.
        /// </summary>
        public InterpalsEntities(string connectionString) : base(connectionString, "InterpalsEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new InterpalsEntities object.
        /// </summary>
        public InterpalsEntities(EntityConnection connection) : base(connection, "InterpalsEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Messages> Messages
        {
            get
            {
                if ((_Messages == null))
                {
                    _Messages = base.CreateObjectSet<Messages>("Messages");
                }
                return _Messages;
            }
        }
        private ObjectSet<Messages> _Messages;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Users> Users
        {
            get
            {
                if ((_Users == null))
                {
                    _Users = base.CreateObjectSet<Users>("Users");
                }
                return _Users;
            }
        }
        private ObjectSet<Users> _Users;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Messages EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToMessages(Messages messages)
        {
            base.AddObject("Messages", messages);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Users EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToUsers(Users users)
        {
            base.AddObject("Users", users);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="InterpalsModel", Name="Messages")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Messages : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Messages object.
        /// </summary>
        /// <param name="iD_Message">Initial value of the ID_Message property.</param>
        /// <param name="iD_Thread">Initial value of the ID_Thread property.</param>
        /// <param name="from">Initial value of the From property.</param>
        /// <param name="time">Initial value of the Time property.</param>
        /// <param name="text">Initial value of the Text property.</param>
        /// <param name="id">Initial value of the ID property.</param>
        public static Messages CreateMessages(global::System.String iD_Message, global::System.String iD_Thread, global::System.String from, global::System.String time, global::System.String text, global::System.Int64 id)
        {
            Messages messages = new Messages();
            messages.ID_Message = iD_Message;
            messages.ID_Thread = iD_Thread;
            messages.From = from;
            messages.Time = time;
            messages.Text = text;
            messages.ID = id;
            return messages;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ID_Message
        {
            get
            {
                return _ID_Message;
            }
            set
            {
                OnID_MessageChanging(value);
                ReportPropertyChanging("ID_Message");
                _ID_Message = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ID_Message");
                OnID_MessageChanged();
            }
        }
        private global::System.String _ID_Message;
        partial void OnID_MessageChanging(global::System.String value);
        partial void OnID_MessageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ID_Thread
        {
            get
            {
                return _ID_Thread;
            }
            set
            {
                OnID_ThreadChanging(value);
                ReportPropertyChanging("ID_Thread");
                _ID_Thread = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ID_Thread");
                OnID_ThreadChanged();
            }
        }
        private global::System.String _ID_Thread;
        partial void OnID_ThreadChanging(global::System.String value);
        partial void OnID_ThreadChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String From
        {
            get
            {
                return _From;
            }
            set
            {
                OnFromChanging(value);
                ReportPropertyChanging("From");
                _From = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("From");
                OnFromChanged();
            }
        }
        private global::System.String _From;
        partial void OnFromChanging(global::System.String value);
        partial void OnFromChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Time
        {
            get
            {
                return _Time;
            }
            set
            {
                OnTimeChanging(value);
                ReportPropertyChanging("Time");
                _Time = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Time");
                OnTimeChanged();
            }
        }
        private global::System.String _Time;
        partial void OnTimeChanging(global::System.String value);
        partial void OnTimeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                OnTextChanging(value);
                ReportPropertyChanging("Text");
                _Text = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Text");
                OnTextChanged();
            }
        }
        private global::System.String _Text;
        partial void OnTextChanging(global::System.String value);
        partial void OnTextChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int64 _ID;
        partial void OnIDChanging(global::System.Int64 value);
        partial void OnIDChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="InterpalsModel", Name="Users")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Users : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Users object.
        /// </summary>
        /// <param name="iD_User">Initial value of the ID_User property.</param>
        /// <param name="nickName">Initial value of the NickName property.</param>
        /// <param name="iD_Thread">Initial value of the ID_Thread property.</param>
        /// <param name="age">Initial value of the Age property.</param>
        /// <param name="photo">Initial value of the Photo property.</param>
        /// <param name="country">Initial value of the Country property.</param>
        /// <param name="city">Initial value of the City property.</param>
        public static Users CreateUsers(global::System.String iD_User, global::System.String nickName, global::System.String iD_Thread, global::System.Int64 age, global::System.String photo, global::System.String country, global::System.String city)
        {
            Users users = new Users();
            users.ID_User = iD_User;
            users.NickName = nickName;
            users.ID_Thread = iD_Thread;
            users.Age = age;
            users.Photo = photo;
            users.Country = country;
            users.City = city;
            return users;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ID_User
        {
            get
            {
                return _ID_User;
            }
            set
            {
                if (_ID_User != value)
                {
                    OnID_UserChanging(value);
                    ReportPropertyChanging("ID_User");
                    _ID_User = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("ID_User");
                    OnID_UserChanged();
                }
            }
        }
        private global::System.String _ID_User;
        partial void OnID_UserChanging(global::System.String value);
        partial void OnID_UserChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                if (_NickName != value)
                {
                    OnNickNameChanging(value);
                    ReportPropertyChanging("NickName");
                    _NickName = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("NickName");
                    OnNickNameChanged();
                }
            }
        }
        private global::System.String _NickName;
        partial void OnNickNameChanging(global::System.String value);
        partial void OnNickNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ID_Thread
        {
            get
            {
                return _ID_Thread;
            }
            set
            {
                if (_ID_Thread != value)
                {
                    OnID_ThreadChanging(value);
                    ReportPropertyChanging("ID_Thread");
                    _ID_Thread = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("ID_Thread");
                    OnID_ThreadChanged();
                }
            }
        }
        private global::System.String _ID_Thread;
        partial void OnID_ThreadChanging(global::System.String value);
        partial void OnID_ThreadChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 Age
        {
            get
            {
                return _Age;
            }
            set
            {
                if (_Age != value)
                {
                    OnAgeChanging(value);
                    ReportPropertyChanging("Age");
                    _Age = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Age");
                    OnAgeChanged();
                }
            }
        }
        private global::System.Int64 _Age;
        partial void OnAgeChanging(global::System.Int64 value);
        partial void OnAgeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Photo
        {
            get
            {
                return _Photo;
            }
            set
            {
                if (_Photo != value)
                {
                    OnPhotoChanging(value);
                    ReportPropertyChanging("Photo");
                    _Photo = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("Photo");
                    OnPhotoChanged();
                }
            }
        }
        private global::System.String _Photo;
        partial void OnPhotoChanging(global::System.String value);
        partial void OnPhotoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Country
        {
            get
            {
                return _Country;
            }
            set
            {
                if (_Country != value)
                {
                    OnCountryChanging(value);
                    ReportPropertyChanging("Country");
                    _Country = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("Country");
                    OnCountryChanged();
                }
            }
        }
        private global::System.String _Country;
        partial void OnCountryChanging(global::System.String value);
        partial void OnCountryChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String City
        {
            get
            {
                return _City;
            }
            set
            {
                if (_City != value)
                {
                    OnCityChanging(value);
                    ReportPropertyChanging("City");
                    _City = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("City");
                    OnCityChanged();
                }
            }
        }
        private global::System.String _City;
        partial void OnCityChanging(global::System.String value);
        partial void OnCityChanged();

        #endregion

    
    }

    #endregion

    
}