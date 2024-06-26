﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Parking_Finals
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="mallparking")]
	public partial class mallparkingDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCustomer(Customer instance);
    partial void UpdateCustomer(Customer instance);
    partial void DeleteCustomer(Customer instance);
    partial void InsertParkingArea(ParkingArea instance);
    partial void UpdateParkingArea(ParkingArea instance);
    partial void DeleteParkingArea(ParkingArea instance);
    partial void InsertPlate_Number(Plate_Number instance);
    partial void UpdatePlate_Number(Plate_Number instance);
    partial void DeletePlate_Number(Plate_Number instance);
    partial void InsertReceipt(Receipt instance);
    partial void UpdateReceipt(Receipt instance);
    partial void DeleteReceipt(Receipt instance);
    partial void InsertStaff(Staff instance);
    partial void UpdateStaff(Staff instance);
    partial void DeleteStaff(Staff instance);
    #endregion
		
		public mallparkingDataContext() : 
				base(global::Parking_Finals.Properties.Settings.Default.mallparkingConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public mallparkingDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public mallparkingDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public mallparkingDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public mallparkingDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Customer> Customers
		{
			get
			{
				return this.GetTable<Customer>();
			}
		}
		
		public System.Data.Linq.Table<ParkingArea> ParkingAreas
		{
			get
			{
				return this.GetTable<ParkingArea>();
			}
		}
		
		public System.Data.Linq.Table<Plate_Number> Plate_Numbers
		{
			get
			{
				return this.GetTable<Plate_Number>();
			}
		}
		
		public System.Data.Linq.Table<Receipt> Receipts
		{
			get
			{
				return this.GetTable<Receipt>();
			}
		}
		
		public System.Data.Linq.Table<Staff> Staffs
		{
			get
			{
				return this.GetTable<Staff>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Customer")]
	public partial class Customer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Customer_ID;
		
		private string _Customer_Name;
		
		private string _Plate_Number;
		
		private string _Receipt_ID;
		
		private string _Contact_Number;
		
		private string _OR_CR;
		
		private EntityRef<Plate_Number> _Plate_Number1;
		
		private EntityRef<Receipt> _Receipt;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCustomer_IDChanging(string value);
    partial void OnCustomer_IDChanged();
    partial void OnCustomer_NameChanging(string value);
    partial void OnCustomer_NameChanged();
    partial void OnPlate_NumberChanging(string value);
    partial void OnPlate_NumberChanged();
    partial void OnReceipt_IDChanging(string value);
    partial void OnReceipt_IDChanged();
    partial void OnContact_NumberChanging(string value);
    partial void OnContact_NumberChanged();
    partial void OnOR_CRChanging(string value);
    partial void OnOR_CRChanged();
    #endregion
		
		public Customer()
		{
			this._Plate_Number1 = default(EntityRef<Plate_Number>);
			this._Receipt = default(EntityRef<Receipt>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_ID", DbType="VarChar(100) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Customer_ID
		{
			get
			{
				return this._Customer_ID;
			}
			set
			{
				if ((this._Customer_ID != value))
				{
					this.OnCustomer_IDChanging(value);
					this.SendPropertyChanging();
					this._Customer_ID = value;
					this.SendPropertyChanged("Customer_ID");
					this.OnCustomer_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_Name", DbType="VarChar(100)")]
		public string Customer_Name
		{
			get
			{
				return this._Customer_Name;
			}
			set
			{
				if ((this._Customer_Name != value))
				{
					this.OnCustomer_NameChanging(value);
					this.SendPropertyChanging();
					this._Customer_Name = value;
					this.SendPropertyChanged("Customer_Name");
					this.OnCustomer_NameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Plate_Number", DbType="VarChar(100)")]
		public string Plate_Number
		{
			get
			{
				return this._Plate_Number;
			}
			set
			{
				if ((this._Plate_Number != value))
				{
					if (this._Plate_Number1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPlate_NumberChanging(value);
					this.SendPropertyChanging();
					this._Plate_Number = value;
					this.SendPropertyChanged("Plate_Number");
					this.OnPlate_NumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Receipt_ID", DbType="VarChar(100)")]
		public string Receipt_ID
		{
			get
			{
				return this._Receipt_ID;
			}
			set
			{
				if ((this._Receipt_ID != value))
				{
					if (this._Receipt.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnReceipt_IDChanging(value);
					this.SendPropertyChanging();
					this._Receipt_ID = value;
					this.SendPropertyChanged("Receipt_ID");
					this.OnReceipt_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Contact_Number", DbType="VarChar(50)")]
		public string Contact_Number
		{
			get
			{
				return this._Contact_Number;
			}
			set
			{
				if ((this._Contact_Number != value))
				{
					this.OnContact_NumberChanging(value);
					this.SendPropertyChanging();
					this._Contact_Number = value;
					this.SendPropertyChanged("Contact_Number");
					this.OnContact_NumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[OR/CR]", Storage="_OR_CR", DbType="VarChar(MAX)")]
		public string OR_CR
		{
			get
			{
				return this._OR_CR;
			}
			set
			{
				if ((this._OR_CR != value))
				{
					this.OnOR_CRChanging(value);
					this.SendPropertyChanging();
					this._OR_CR = value;
					this.SendPropertyChanged("OR_CR");
					this.OnOR_CRChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Plate_Number_Customer", Storage="_Plate_Number1", ThisKey="Plate_Number", OtherKey="Plate_Number1", IsForeignKey=true)]
		public Plate_Number Plate_Number1
		{
			get
			{
				return this._Plate_Number1.Entity;
			}
			set
			{
				Plate_Number previousValue = this._Plate_Number1.Entity;
				if (((previousValue != value) 
							|| (this._Plate_Number1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Plate_Number1.Entity = null;
						previousValue.Customers.Remove(this);
					}
					this._Plate_Number1.Entity = value;
					if ((value != null))
					{
						value.Customers.Add(this);
						this._Plate_Number = value.Plate_Number1;
					}
					else
					{
						this._Plate_Number = default(string);
					}
					this.SendPropertyChanged("Plate_Number1");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Receipt_Customer", Storage="_Receipt", ThisKey="Receipt_ID", OtherKey="Receipt_ID", IsForeignKey=true)]
		public Receipt Receipt
		{
			get
			{
				return this._Receipt.Entity;
			}
			set
			{
				Receipt previousValue = this._Receipt.Entity;
				if (((previousValue != value) 
							|| (this._Receipt.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Receipt.Entity = null;
						previousValue.Customers.Remove(this);
					}
					this._Receipt.Entity = value;
					if ((value != null))
					{
						value.Customers.Add(this);
						this._Receipt_ID = value.Receipt_ID;
					}
					else
					{
						this._Receipt_ID = default(string);
					}
					this.SendPropertyChanged("Receipt");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ParkingArea")]
	public partial class ParkingArea : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _ParkingArea_ID;
		
		private string _ParkingArea_Location;
		
		private int _ParkingArea_Capacity;
		
		private int _Available_Slot;
		
		private EntitySet<Receipt> _Receipts;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnParkingArea_IDChanging(string value);
    partial void OnParkingArea_IDChanged();
    partial void OnParkingArea_LocationChanging(string value);
    partial void OnParkingArea_LocationChanged();
    partial void OnParkingArea_CapacityChanging(int value);
    partial void OnParkingArea_CapacityChanged();
    partial void OnAvailable_SlotChanging(int value);
    partial void OnAvailable_SlotChanged();
    #endregion
		
		public ParkingArea()
		{
			this._Receipts = new EntitySet<Receipt>(new Action<Receipt>(this.attach_Receipts), new Action<Receipt>(this.detach_Receipts));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParkingArea_ID", DbType="VarChar(100) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string ParkingArea_ID
		{
			get
			{
				return this._ParkingArea_ID;
			}
			set
			{
				if ((this._ParkingArea_ID != value))
				{
					this.OnParkingArea_IDChanging(value);
					this.SendPropertyChanging();
					this._ParkingArea_ID = value;
					this.SendPropertyChanged("ParkingArea_ID");
					this.OnParkingArea_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParkingArea_Location", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string ParkingArea_Location
		{
			get
			{
				return this._ParkingArea_Location;
			}
			set
			{
				if ((this._ParkingArea_Location != value))
				{
					this.OnParkingArea_LocationChanging(value);
					this.SendPropertyChanging();
					this._ParkingArea_Location = value;
					this.SendPropertyChanged("ParkingArea_Location");
					this.OnParkingArea_LocationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParkingArea_Capacity", DbType="Int NOT NULL")]
		public int ParkingArea_Capacity
		{
			get
			{
				return this._ParkingArea_Capacity;
			}
			set
			{
				if ((this._ParkingArea_Capacity != value))
				{
					this.OnParkingArea_CapacityChanging(value);
					this.SendPropertyChanging();
					this._ParkingArea_Capacity = value;
					this.SendPropertyChanged("ParkingArea_Capacity");
					this.OnParkingArea_CapacityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Available_Slot", DbType="Int NOT NULL")]
		public int Available_Slot
		{
			get
			{
				return this._Available_Slot;
			}
			set
			{
				if ((this._Available_Slot != value))
				{
					this.OnAvailable_SlotChanging(value);
					this.SendPropertyChanging();
					this._Available_Slot = value;
					this.SendPropertyChanged("Available_Slot");
					this.OnAvailable_SlotChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ParkingArea_Receipt", Storage="_Receipts", ThisKey="ParkingArea_ID", OtherKey="ParkingArea_ID")]
		public EntitySet<Receipt> Receipts
		{
			get
			{
				return this._Receipts;
			}
			set
			{
				this._Receipts.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Receipts(Receipt entity)
		{
			this.SendPropertyChanging();
			entity.ParkingArea = this;
		}
		
		private void detach_Receipts(Receipt entity)
		{
			this.SendPropertyChanging();
			entity.ParkingArea = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Plate_Number")]
	public partial class Plate_Number : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Plate_Number1;
		
		private string _Car_Photo;
		
		private EntitySet<Customer> _Customers;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPlate_Number1Changing(string value);
    partial void OnPlate_Number1Changed();
    partial void OnCar_PhotoChanging(string value);
    partial void OnCar_PhotoChanged();
    #endregion
		
		public Plate_Number()
		{
			this._Customers = new EntitySet<Customer>(new Action<Customer>(this.attach_Customers), new Action<Customer>(this.detach_Customers));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Plate_Number", Storage="_Plate_Number1", DbType="VarChar(100) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Plate_Number1
		{
			get
			{
				return this._Plate_Number1;
			}
			set
			{
				if ((this._Plate_Number1 != value))
				{
					this.OnPlate_Number1Changing(value);
					this.SendPropertyChanging();
					this._Plate_Number1 = value;
					this.SendPropertyChanged("Plate_Number1");
					this.OnPlate_Number1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Car_Photo", DbType="VarChar(255)")]
		public string Car_Photo
		{
			get
			{
				return this._Car_Photo;
			}
			set
			{
				if ((this._Car_Photo != value))
				{
					this.OnCar_PhotoChanging(value);
					this.SendPropertyChanging();
					this._Car_Photo = value;
					this.SendPropertyChanged("Car_Photo");
					this.OnCar_PhotoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Plate_Number_Customer", Storage="_Customers", ThisKey="Plate_Number1", OtherKey="Plate_Number")]
		public EntitySet<Customer> Customers
		{
			get
			{
				return this._Customers;
			}
			set
			{
				this._Customers.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Customers(Customer entity)
		{
			this.SendPropertyChanging();
			entity.Plate_Number1 = this;
		}
		
		private void detach_Customers(Customer entity)
		{
			this.SendPropertyChanging();
			entity.Plate_Number1 = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Receipt")]
	public partial class Receipt : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Receipt_ID;
		
		private string _Payment_Method;
		
		private string _Payment_Status;
		
		private System.Nullable<decimal> _Total_Amount;
		
		private string _Change_Amount;
		
		private string _Staff_ID;
		
		private string _ParkingArea_ID;
		
		private string _Parking_Status;
		
		private System.Nullable<System.DateTime> _Time_IN;
		
		private System.Nullable<System.DateTime> _Time_Out;
		
		private EntitySet<Customer> _Customers;
		
		private EntityRef<ParkingArea> _ParkingArea;
		
		private EntityRef<Staff> _Staff;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnReceipt_IDChanging(string value);
    partial void OnReceipt_IDChanged();
    partial void OnPayment_MethodChanging(string value);
    partial void OnPayment_MethodChanged();
    partial void OnPayment_StatusChanging(string value);
    partial void OnPayment_StatusChanged();
    partial void OnTotal_AmountChanging(System.Nullable<decimal> value);
    partial void OnTotal_AmountChanged();
    partial void OnChange_AmountChanging(string value);
    partial void OnChange_AmountChanged();
    partial void OnStaff_IDChanging(string value);
    partial void OnStaff_IDChanged();
    partial void OnParkingArea_IDChanging(string value);
    partial void OnParkingArea_IDChanged();
    partial void OnParking_StatusChanging(string value);
    partial void OnParking_StatusChanged();
    partial void OnTime_INChanging(System.Nullable<System.DateTime> value);
    partial void OnTime_INChanged();
    partial void OnTime_OutChanging(System.Nullable<System.DateTime> value);
    partial void OnTime_OutChanged();
    #endregion
		
		public Receipt()
		{
			this._Customers = new EntitySet<Customer>(new Action<Customer>(this.attach_Customers), new Action<Customer>(this.detach_Customers));
			this._ParkingArea = default(EntityRef<ParkingArea>);
			this._Staff = default(EntityRef<Staff>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Receipt_ID", DbType="VarChar(100) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Receipt_ID
		{
			get
			{
				return this._Receipt_ID;
			}
			set
			{
				if ((this._Receipt_ID != value))
				{
					this.OnReceipt_IDChanging(value);
					this.SendPropertyChanging();
					this._Receipt_ID = value;
					this.SendPropertyChanged("Receipt_ID");
					this.OnReceipt_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Payment_Method", DbType="VarChar(50)")]
		public string Payment_Method
		{
			get
			{
				return this._Payment_Method;
			}
			set
			{
				if ((this._Payment_Method != value))
				{
					this.OnPayment_MethodChanging(value);
					this.SendPropertyChanging();
					this._Payment_Method = value;
					this.SendPropertyChanged("Payment_Method");
					this.OnPayment_MethodChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Payment_Status", DbType="VarChar(50)")]
		public string Payment_Status
		{
			get
			{
				return this._Payment_Status;
			}
			set
			{
				if ((this._Payment_Status != value))
				{
					this.OnPayment_StatusChanging(value);
					this.SendPropertyChanging();
					this._Payment_Status = value;
					this.SendPropertyChanged("Payment_Status");
					this.OnPayment_StatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Total_Amount", DbType="Decimal(10,2)")]
		public System.Nullable<decimal> Total_Amount
		{
			get
			{
				return this._Total_Amount;
			}
			set
			{
				if ((this._Total_Amount != value))
				{
					this.OnTotal_AmountChanging(value);
					this.SendPropertyChanging();
					this._Total_Amount = value;
					this.SendPropertyChanged("Total_Amount");
					this.OnTotal_AmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Change_Amount", DbType="VarChar(MAX)")]
		public string Change_Amount
		{
			get
			{
				return this._Change_Amount;
			}
			set
			{
				if ((this._Change_Amount != value))
				{
					this.OnChange_AmountChanging(value);
					this.SendPropertyChanging();
					this._Change_Amount = value;
					this.SendPropertyChanged("Change_Amount");
					this.OnChange_AmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Staff_ID", DbType="VarChar(100)")]
		public string Staff_ID
		{
			get
			{
				return this._Staff_ID;
			}
			set
			{
				if ((this._Staff_ID != value))
				{
					if (this._Staff.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnStaff_IDChanging(value);
					this.SendPropertyChanging();
					this._Staff_ID = value;
					this.SendPropertyChanged("Staff_ID");
					this.OnStaff_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParkingArea_ID", DbType="VarChar(100)")]
		public string ParkingArea_ID
		{
			get
			{
				return this._ParkingArea_ID;
			}
			set
			{
				if ((this._ParkingArea_ID != value))
				{
					if (this._ParkingArea.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnParkingArea_IDChanging(value);
					this.SendPropertyChanging();
					this._ParkingArea_ID = value;
					this.SendPropertyChanged("ParkingArea_ID");
					this.OnParkingArea_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Parking_Status", DbType="VarChar(100)")]
		public string Parking_Status
		{
			get
			{
				return this._Parking_Status;
			}
			set
			{
				if ((this._Parking_Status != value))
				{
					this.OnParking_StatusChanging(value);
					this.SendPropertyChanging();
					this._Parking_Status = value;
					this.SendPropertyChanged("Parking_Status");
					this.OnParking_StatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Time_IN", DbType="DateTime")]
		public System.Nullable<System.DateTime> Time_IN
		{
			get
			{
				return this._Time_IN;
			}
			set
			{
				if ((this._Time_IN != value))
				{
					this.OnTime_INChanging(value);
					this.SendPropertyChanging();
					this._Time_IN = value;
					this.SendPropertyChanged("Time_IN");
					this.OnTime_INChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Time_Out", DbType="DateTime")]
		public System.Nullable<System.DateTime> Time_Out
		{
			get
			{
				return this._Time_Out;
			}
			set
			{
				if ((this._Time_Out != value))
				{
					this.OnTime_OutChanging(value);
					this.SendPropertyChanging();
					this._Time_Out = value;
					this.SendPropertyChanged("Time_Out");
					this.OnTime_OutChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Receipt_Customer", Storage="_Customers", ThisKey="Receipt_ID", OtherKey="Receipt_ID")]
		public EntitySet<Customer> Customers
		{
			get
			{
				return this._Customers;
			}
			set
			{
				this._Customers.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ParkingArea_Receipt", Storage="_ParkingArea", ThisKey="ParkingArea_ID", OtherKey="ParkingArea_ID", IsForeignKey=true)]
		public ParkingArea ParkingArea
		{
			get
			{
				return this._ParkingArea.Entity;
			}
			set
			{
				ParkingArea previousValue = this._ParkingArea.Entity;
				if (((previousValue != value) 
							|| (this._ParkingArea.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ParkingArea.Entity = null;
						previousValue.Receipts.Remove(this);
					}
					this._ParkingArea.Entity = value;
					if ((value != null))
					{
						value.Receipts.Add(this);
						this._ParkingArea_ID = value.ParkingArea_ID;
					}
					else
					{
						this._ParkingArea_ID = default(string);
					}
					this.SendPropertyChanged("ParkingArea");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Staff_Receipt", Storage="_Staff", ThisKey="Staff_ID", OtherKey="Staff_ID", IsForeignKey=true)]
		public Staff Staff
		{
			get
			{
				return this._Staff.Entity;
			}
			set
			{
				Staff previousValue = this._Staff.Entity;
				if (((previousValue != value) 
							|| (this._Staff.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Staff.Entity = null;
						previousValue.Receipts.Remove(this);
					}
					this._Staff.Entity = value;
					if ((value != null))
					{
						value.Receipts.Add(this);
						this._Staff_ID = value.Staff_ID;
					}
					else
					{
						this._Staff_ID = default(string);
					}
					this.SendPropertyChanged("Staff");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Customers(Customer entity)
		{
			this.SendPropertyChanging();
			entity.Receipt = this;
		}
		
		private void detach_Customers(Customer entity)
		{
			this.SendPropertyChanging();
			entity.Receipt = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Staff")]
	public partial class Staff : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Staff_ID;
		
		private string _Staff_Role;
		
		private string _Staff_Name;
		
		private string _Staff_Username;
		
		private string _Staff_Password;
		
		private string _Staff_Status;
		
		private EntitySet<Receipt> _Receipts;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnStaff_IDChanging(string value);
    partial void OnStaff_IDChanged();
    partial void OnStaff_RoleChanging(string value);
    partial void OnStaff_RoleChanged();
    partial void OnStaff_NameChanging(string value);
    partial void OnStaff_NameChanged();
    partial void OnStaff_UsernameChanging(string value);
    partial void OnStaff_UsernameChanged();
    partial void OnStaff_PasswordChanging(string value);
    partial void OnStaff_PasswordChanged();
    partial void OnStaff_StatusChanging(string value);
    partial void OnStaff_StatusChanged();
    #endregion
		
		public Staff()
		{
			this._Receipts = new EntitySet<Receipt>(new Action<Receipt>(this.attach_Receipts), new Action<Receipt>(this.detach_Receipts));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Staff_ID", DbType="VarChar(100) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Staff_ID
		{
			get
			{
				return this._Staff_ID;
			}
			set
			{
				if ((this._Staff_ID != value))
				{
					this.OnStaff_IDChanging(value);
					this.SendPropertyChanging();
					this._Staff_ID = value;
					this.SendPropertyChanged("Staff_ID");
					this.OnStaff_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Staff_Role", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Staff_Role
		{
			get
			{
				return this._Staff_Role;
			}
			set
			{
				if ((this._Staff_Role != value))
				{
					this.OnStaff_RoleChanging(value);
					this.SendPropertyChanging();
					this._Staff_Role = value;
					this.SendPropertyChanged("Staff_Role");
					this.OnStaff_RoleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Staff_Name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Staff_Name
		{
			get
			{
				return this._Staff_Name;
			}
			set
			{
				if ((this._Staff_Name != value))
				{
					this.OnStaff_NameChanging(value);
					this.SendPropertyChanging();
					this._Staff_Name = value;
					this.SendPropertyChanged("Staff_Name");
					this.OnStaff_NameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Staff_Username", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Staff_Username
		{
			get
			{
				return this._Staff_Username;
			}
			set
			{
				if ((this._Staff_Username != value))
				{
					this.OnStaff_UsernameChanging(value);
					this.SendPropertyChanging();
					this._Staff_Username = value;
					this.SendPropertyChanged("Staff_Username");
					this.OnStaff_UsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Staff_Password", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Staff_Password
		{
			get
			{
				return this._Staff_Password;
			}
			set
			{
				if ((this._Staff_Password != value))
				{
					this.OnStaff_PasswordChanging(value);
					this.SendPropertyChanging();
					this._Staff_Password = value;
					this.SendPropertyChanged("Staff_Password");
					this.OnStaff_PasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Staff_Status", DbType="VarChar(100)")]
		public string Staff_Status
		{
			get
			{
				return this._Staff_Status;
			}
			set
			{
				if ((this._Staff_Status != value))
				{
					this.OnStaff_StatusChanging(value);
					this.SendPropertyChanging();
					this._Staff_Status = value;
					this.SendPropertyChanged("Staff_Status");
					this.OnStaff_StatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Staff_Receipt", Storage="_Receipts", ThisKey="Staff_ID", OtherKey="Staff_ID")]
		public EntitySet<Receipt> Receipts
		{
			get
			{
				return this._Receipts;
			}
			set
			{
				this._Receipts.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Receipts(Receipt entity)
		{
			this.SendPropertyChanging();
			entity.Staff = this;
		}
		
		private void detach_Receipts(Receipt entity)
		{
			this.SendPropertyChanging();
			entity.Staff = null;
		}
	}
}
#pragma warning restore 1591
