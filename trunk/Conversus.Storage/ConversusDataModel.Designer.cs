﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("conversusModel", "FK_Clients_0_0", "Queues", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Conversus.Storage.Queue), "Clients", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Conversus.Storage.Client), true)]
[assembly: EdmRelationshipAttribute("conversusModel", "FK_Users_0_0", "Queues", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Conversus.Storage.Queue), "Users", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Conversus.Storage.User), true)]

#endregion

namespace Conversus.Storage
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class conversusEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new conversusEntities object using the connection string found in the 'conversusEntities' section of the application configuration file.
        /// </summary>
        public conversusEntities() : base("name=conversusEntities", "conversusEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new conversusEntities object.
        /// </summary>
        public conversusEntities(string connectionString) : base(connectionString, "conversusEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new conversusEntities object.
        /// </summary>
        public conversusEntities(EntityConnection connection) : base(connection, "conversusEntities")
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
        public ObjectSet<Client> Clients
        {
            get
            {
                if ((_Clients == null))
                {
                    _Clients = base.CreateObjectSet<Client>("Clients");
                }
                return _Clients;
            }
        }
        private ObjectSet<Client> _Clients;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Queue> Queues
        {
            get
            {
                if ((_Queues == null))
                {
                    _Queues = base.CreateObjectSet<Queue>("Queues");
                }
                return _Queues;
            }
        }
        private ObjectSet<Queue> _Queues;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<User> Users
        {
            get
            {
                if ((_Users == null))
                {
                    _Users = base.CreateObjectSet<User>("Users");
                }
                return _Users;
            }
        }
        private ObjectSet<User> _Users;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Clients EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToClients(Client client)
        {
            base.AddObject("Clients", client);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Queues EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToQueues(Queue queue)
        {
            base.AddObject("Queues", queue);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Users EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToUsers(User user)
        {
            base.AddObject("Users", user);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="conversusModel", Name="Client")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Client : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Client object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="queueId">Initial value of the QueueId property.</param>
        /// <param name="status">Initial value of the Status property.</param>
        /// <param name="bookingTime">Initial value of the BookingTime property.</param>
        public static Client CreateClient(global::System.Guid id, global::System.String name, global::System.Guid queueId, global::System.Int32 status, global::System.DateTime bookingTime)
        {
            Client client = new Client();
            client.Id = id;
            client.Name = name;
            client.QueueId = queueId;
            client.Status = status;
            client.BookingTime = bookingTime;
            return client;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid QueueId
        {
            get
            {
                return _QueueId;
            }
            set
            {
                OnQueueIdChanging(value);
                ReportPropertyChanging("QueueId");
                _QueueId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("QueueId");
                OnQueueIdChanged();
            }
        }
        private global::System.Guid _QueueId;
        partial void OnQueueIdChanging(global::System.Guid value);
        partial void OnQueueIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Status
        {
            get
            {
                return _Status;
            }
            set
            {
                OnStatusChanging(value);
                ReportPropertyChanging("Status");
                _Status = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Status");
                OnStatusChanged();
            }
        }
        private global::System.Int32 _Status;
        partial void OnStatusChanging(global::System.Int32 value);
        partial void OnStatusChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PIN
        {
            get
            {
                return _PIN;
            }
            set
            {
                OnPINChanging(value);
                ReportPropertyChanging("PIN");
                _PIN = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PIN");
                OnPINChanged();
            }
        }
        private Nullable<global::System.Int32> _PIN;
        partial void OnPINChanging(Nullable<global::System.Int32> value);
        partial void OnPINChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Ticket
        {
            get
            {
                return _Ticket;
            }
            set
            {
                OnTicketChanging(value);
                ReportPropertyChanging("Ticket");
                _Ticket = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Ticket");
                OnTicketChanged();
            }
        }
        private global::System.String _Ticket;
        partial void OnTicketChanging(global::System.String value);
        partial void OnTicketChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> PerformStart
        {
            get
            {
                return _PerformStart;
            }
            set
            {
                OnPerformStartChanging(value);
                ReportPropertyChanging("PerformStart");
                _PerformStart = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PerformStart");
                OnPerformStartChanged();
            }
        }
        private Nullable<global::System.DateTime> _PerformStart;
        partial void OnPerformStartChanging(Nullable<global::System.DateTime> value);
        partial void OnPerformStartChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> PerformEnd
        {
            get
            {
                return _PerformEnd;
            }
            set
            {
                OnPerformEndChanging(value);
                ReportPropertyChanging("PerformEnd");
                _PerformEnd = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PerformEnd");
                OnPerformEndChanged();
            }
        }
        private Nullable<global::System.DateTime> _PerformEnd;
        partial void OnPerformEndChanging(Nullable<global::System.DateTime> value);
        partial void OnPerformEndChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime BookingTime
        {
            get
            {
                return _BookingTime;
            }
            set
            {
                OnBookingTimeChanging(value);
                ReportPropertyChanging("BookingTime");
                _BookingTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("BookingTime");
                OnBookingTimeChanged();
            }
        }
        private global::System.DateTime _BookingTime;
        partial void OnBookingTimeChanging(global::System.DateTime value);
        partial void OnBookingTimeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> TakeTicket
        {
            get
            {
                return _TakeTicket;
            }
            set
            {
                OnTakeTicketChanging(value);
                ReportPropertyChanging("TakeTicket");
                _TakeTicket = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TakeTicket");
                OnTakeTicketChanged();
            }
        }
        private Nullable<global::System.DateTime> _TakeTicket;
        partial void OnTakeTicketChanging(Nullable<global::System.DateTime> value);
        partial void OnTakeTicketChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("conversusModel", "FK_Clients_0_0", "Queues")]
        public Queue Queue
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Queue>("conversusModel.FK_Clients_0_0", "Queues").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Queue>("conversusModel.FK_Clients_0_0", "Queues").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Queue> QueueReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Queue>("conversusModel.FK_Clients_0_0", "Queues");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Queue>("conversusModel.FK_Clients_0_0", "Queues", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="conversusModel", Name="Queue")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Queue : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Queue object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="type">Initial value of the Type property.</param>
        public static Queue CreateQueue(global::System.Guid id, global::System.Int32 type)
        {
            Queue queue = new Queue();
            queue.Id = id;
            queue.Type = type;
            return queue;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Type
        {
            get
            {
                return _Type;
            }
            set
            {
                OnTypeChanging(value);
                ReportPropertyChanging("Type");
                _Type = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Type");
                OnTypeChanged();
            }
        }
        private global::System.Int32 _Type;
        partial void OnTypeChanging(global::System.Int32 value);
        partial void OnTypeChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("conversusModel", "FK_Clients_0_0", "Clients")]
        public EntityCollection<Client> Clients
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Client>("conversusModel.FK_Clients_0_0", "Clients");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Client>("conversusModel.FK_Clients_0_0", "Clients", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("conversusModel", "FK_Users_0_0", "Users")]
        public EntityCollection<User> Users
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<User>("conversusModel.FK_Users_0_0", "Users");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<User>("conversusModel.FK_Users_0_0", "Users", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="conversusModel", Name="User")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class User : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new User object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="login">Initial value of the Login property.</param>
        /// <param name="password">Initial value of the Password property.</param>
        /// <param name="queueId">Initial value of the QueueId property.</param>
        public static User CreateUser(global::System.Guid id, global::System.String name, global::System.String login, global::System.String password, global::System.Guid queueId)
        {
            User user = new User();
            user.Id = id;
            user.Name = name;
            user.Login = login;
            user.Password = password;
            user.QueueId = queueId;
            return user;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Login
        {
            get
            {
                return _Login;
            }
            set
            {
                OnLoginChanging(value);
                ReportPropertyChanging("Login");
                _Login = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Login");
                OnLoginChanged();
            }
        }
        private global::System.String _Login;
        partial void OnLoginChanging(global::System.String value);
        partial void OnLoginChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Password
        {
            get
            {
                return _Password;
            }
            set
            {
                OnPasswordChanging(value);
                ReportPropertyChanging("Password");
                _Password = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Password");
                OnPasswordChanged();
            }
        }
        private global::System.String _Password;
        partial void OnPasswordChanging(global::System.String value);
        partial void OnPasswordChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid QueueId
        {
            get
            {
                return _QueueId;
            }
            set
            {
                OnQueueIdChanging(value);
                ReportPropertyChanging("QueueId");
                _QueueId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("QueueId");
                OnQueueIdChanged();
            }
        }
        private global::System.Guid _QueueId;
        partial void OnQueueIdChanging(global::System.Guid value);
        partial void OnQueueIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Window
        {
            get
            {
                return _Window;
            }
            set
            {
                OnWindowChanging(value);
                ReportPropertyChanging("Window");
                _Window = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Window");
                OnWindowChanged();
            }
        }
        private global::System.String _Window;
        partial void OnWindowChanging(global::System.String value);
        partial void OnWindowChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("conversusModel", "FK_Users_0_0", "Queues")]
        public Queue Queue
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Queue>("conversusModel.FK_Users_0_0", "Queues").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Queue>("conversusModel.FK_Users_0_0", "Queues").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Queue> QueueReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Queue>("conversusModel.FK_Users_0_0", "Queues");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Queue>("conversusModel.FK_Users_0_0", "Queues", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
