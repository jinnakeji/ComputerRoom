/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2020/1/8 11:46:11                            */
/*==============================================================*/


alter table Current_Threshold
   drop constraint PK_CURRENT_THRESHOLD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Current_Threshold')
            and   type = 'U')
   drop table Current_Threshold
go

alter table Device_AirConditioner_Temperature
   drop constraint PK_DEVICE_AIRCONDITIONER_TEMPE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_AirConditioner_Temperature')
            and   type = 'U')
   drop table Device_AirConditioner_Temperature
go

alter table Device_Battery_Condition
   drop constraint PK_DEVICE_BATTERY_CONDITION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Battery_Condition')
            and   type = 'U')
   drop table Device_Battery_Condition
go

alter table Device_Cabinet_SmokeFire
   drop constraint PK_DEVICE_CABINET_SMOKEFIRE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Cabinet_SmokeFire')
            and   type = 'U')
   drop table Device_Cabinet_SmokeFire
go

alter table Device_Cabinet_Temperature
   drop constraint PK_DEVICE_CABINET_TEMPERATURE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Cabinet_Temperature')
            and   type = 'U')
   drop table Device_Cabinet_Temperature
go

alter table Device_Grounding_Resistance
   drop constraint PK_DEVICE_GROUNDING_RESISTANCE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Grounding_Resistance')
            and   type = 'U')
   drop table Device_Grounding_Resistance
go

alter table Device_Indoor_Gas
   drop constraint PK_DEVICE_INDOOR_GAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Indoor_Gas')
            and   type = 'U')
   drop table Device_Indoor_Gas
go

alter table Device_Indoor_Humidity
   drop constraint PK_DEVICE_INDOOR_HUMIDITY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Indoor_Humidity')
            and   type = 'U')
   drop table Device_Indoor_Humidity
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Indoor_LeakWater')
            and   type = 'U')
   drop table Device_Indoor_LeakWater
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_InfraredAlarm')
            and   type = 'U')
   drop table Device_InfraredAlarm
go

alter table Device_Iron_Tower
   drop constraint PK_DEVICE_IRON_TOWER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Iron_Tower')
            and   type = 'U')
   drop table Device_Iron_Tower
go

alter table Device_Power_Cable
   drop constraint PK_DEVICE_POWER_CABLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Power_Cable')
            and   type = 'U')
   drop table Device_Power_Cable
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Switching_State')
            and   type = 'U')
   drop table Device_Switching_State
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Device_Three_Electric')
            and   type = 'U')
   drop table Device_Three_Electric
go

alter table Humidity_Threshold
   drop constraint PK_HUMIDITY_THRESHOLD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Humidity_Threshold')
            and   type = 'U')
   drop table Humidity_Threshold
go

alter table Iron_Tower_Threshold
   drop constraint PK_IRON_TOWER_THRESHOLD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Iron_Tower_Threshold')
            and   type = 'U')
   drop table Iron_Tower_Threshold
go

alter table Resistance_Threshold
   drop constraint PK_RESISTANCE_THRESHOLD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Resistance_Threshold')
            and   type = 'U')
   drop table Resistance_Threshold
go

alter table Temperature_Threshold
   drop constraint PK_TEMPERATURE_THRESHOLD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Temperature_Threshold')
            and   type = 'U')
   drop table Temperature_Threshold
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Three_Electric_Threshold')
            and   type = 'U')
   drop table Three_Electric_Threshold
go

alter table Voltage_Threshold
   drop constraint PK_VOLTAGE_THRESHOLD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Voltage_Threshold')
            and   type = 'U')
   drop table Voltage_Threshold
go

/*==============================================================*/
/* Table: Current_Threshold                                     */
/*==============================================================*/
create table Current_Threshold (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   Highest_Current      nvarchar(50)         null,
   Lowest_Current       nvarchar(50)         null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Current_Threshold') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Current_Threshold' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '电流阈值设置', 
   'user', @CurrentUser, 'table', 'Current_Threshold'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Current_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Current_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Current_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Current_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Current_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Current_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Highest_Current')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'Highest_Current'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最高电流',
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'Highest_Current'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Current_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Lowest_Current')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'Lowest_Current'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最低电流',
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'Lowest_Current'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Current_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '阈值设置时间',
   'user', @CurrentUser, 'table', 'Current_Threshold', 'column', 'DeviceUpDateTime'
go

alter table Current_Threshold
   add constraint PK_CURRENT_THRESHOLD primary key (Id)
go

/*==============================================================*/
/* Table: Device_AirConditioner_Temperature                     */
/*==============================================================*/
create table Device_AirConditioner_Temperature (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   TempeRature          nvarchar(50)         null,
   AlarmState_TempeRature int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_AirConditioner_Temperature') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '空调出风口温度监测', 
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_AirConditioner_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_AirConditioner_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_AirConditioner_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_AirConditioner_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_AirConditioner_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_AirConditioner_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常温度',
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_AirConditioner_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'AlarmState_TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '温度报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'AlarmState_TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_AirConditioner_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_AirConditioner_Temperature', 'column', 'DeviceUpDateTime'
go

alter table Device_AirConditioner_Temperature
   add constraint PK_DEVICE_AIRCONDITIONER_TEMPE primary key (Id)
go

/*==============================================================*/
/* Table: Device_Battery_Condition                              */
/*==============================================================*/
create table Device_Battery_Condition (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   Voltage              nvarchar(50)         null,
   Resistance           nvarchar(50)         null,
   "Current"            char(10)             null,
   Temperature          nvarchar(50)         null,
   AlarmState_Voltage   int                  null,
   AlarmState_Resistance int                  null,
   AlarmState_Current   int                  null,
   AlarmState_Temperature int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Battery_Condition') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Battery_Condition' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '蓄电池状态监测', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Voltage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Voltage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电压',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Voltage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Resistance')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Resistance'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电阻',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Resistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Current')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Current'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电流',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Current'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Temperature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Temperature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '温度',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'Temperature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_Voltage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'AlarmState_Voltage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电压报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'AlarmState_Voltage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_Resistance')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'AlarmState_Resistance'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电阻报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'AlarmState_Resistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_Current')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'AlarmState_Current'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电流报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'AlarmState_Current'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_Temperature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'AlarmState_Temperature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '温度报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'AlarmState_Temperature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Battery_Condition')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Battery_Condition', 'column', 'DeviceUpDateTime'
go

alter table Device_Battery_Condition
   add constraint PK_DEVICE_BATTERY_CONDITION primary key (Id)
go

/*==============================================================*/
/* Table: Device_Cabinet_SmokeFire                              */
/*==============================================================*/
create table Device_Cabinet_SmokeFire (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   DeviceStatus         int                  null,
   AlarmState           int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Cabinet_SmokeFire') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '烟雾火灾监测', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_SmokeFire')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_SmokeFire')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_SmokeFire')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_SmokeFire')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_SmokeFire')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_SmokeFire')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'DeviceStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备运行状态 1 异常 0正常',
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'DeviceStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_SmokeFire')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_SmokeFire')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Cabinet_SmokeFire', 'column', 'DeviceUpDateTime'
go

alter table Device_Cabinet_SmokeFire
   add constraint PK_DEVICE_CABINET_SMOKEFIRE primary key (Id)
go

/*==============================================================*/
/* Table: Device_Cabinet_Temperature                            */
/*==============================================================*/
create table Device_Cabinet_Temperature (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   TempeRature          nvarchar(50)         null,
   AlarmState_TempeRature int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Cabinet_Temperature') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '机柜内部温度监测', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常温度',
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'AlarmState_TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '温度报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'AlarmState_TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Cabinet_Temperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Cabinet_Temperature', 'column', 'DeviceUpDateTime'
go

alter table Device_Cabinet_Temperature
   add constraint PK_DEVICE_CABINET_TEMPERATURE primary key (Id)
go

/*==============================================================*/
/* Table: Device_Grounding_Resistance                           */
/*==============================================================*/
create table Device_Grounding_Resistance (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   Resistance           nvarchar(50)         null,
   AlarmState_Resistance int                  null,
   DeviceStatus         int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Grounding_Resistance') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '机房接地电阻监测', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Grounding_Resistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Grounding_Resistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Grounding_Resistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Grounding_Resistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Grounding_Resistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Grounding_Resistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Resistance')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'Resistance'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常电阻',
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'Resistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Grounding_Resistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_Resistance')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'AlarmState_Resistance'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电阻报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'AlarmState_Resistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Grounding_Resistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'DeviceStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备运行状态1 异常 0正常',
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'DeviceStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Grounding_Resistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Grounding_Resistance', 'column', 'DeviceUpDateTime'
go

alter table Device_Grounding_Resistance
   add constraint PK_DEVICE_GROUNDING_RESISTANCE primary key (Id)
go

/*==============================================================*/
/* Table: Device_Indoor_Gas                                     */
/*==============================================================*/
create table Device_Indoor_Gas (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   CarbonDioxideValue   nvarchar(50)         null,
   DeviceMacCode        nvarchar(200)        null,
   AlarmState           int                  null,
   DeviceStatus         int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Indoor_Gas') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '室内有毒有害气体监测', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CarbonDioxideValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'CarbonDioxideValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '二氧化碳值',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'CarbonDioxideValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceMacCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'DeviceMacCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备MAC地址',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'DeviceMacCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'DeviceStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备运行状态1 异常 0正常',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'DeviceStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Gas')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Indoor_Gas', 'column', 'DeviceUpDateTime'
go

alter table Device_Indoor_Gas
   add constraint PK_DEVICE_INDOOR_GAS primary key (Id)
go

/*==============================================================*/
/* Table: Device_Indoor_Humidity                                */
/*==============================================================*/
create table Device_Indoor_Humidity (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   TempeRature          nvarchar(50)         null,
   AlarmState_TempeRature int                  null,
   Humidity             nvarchar(50)         null,
   AlarmState_Humidity  int                  null,
   DeviceStatus         int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Indoor_Humidity') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '室内温湿度监测', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常温度',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'AlarmState_TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '温度报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'AlarmState_TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Humidity')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'Humidity'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常湿度',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'Humidity'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_Humidity')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'AlarmState_Humidity'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '湿度报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'AlarmState_Humidity'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'DeviceStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备运行状态1 异常 0正常',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'DeviceStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_Humidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Indoor_Humidity', 'column', 'DeviceUpDateTime'
go

alter table Device_Indoor_Humidity
   add constraint PK_DEVICE_INDOOR_HUMIDITY primary key (Id)
go

/*==============================================================*/
/* Table: Device_Indoor_LeakWater                               */
/*==============================================================*/
create table Device_Indoor_LeakWater (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   AlarmState           int                  null,
   DeviceStatus         int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Indoor_LeakWater') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '室内漏水监测', 
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_LeakWater')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_LeakWater')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_LeakWater')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_LeakWater')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_LeakWater')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_LeakWater')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_LeakWater')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'DeviceStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备运行状态 1 异常 0正常',
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'DeviceStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Indoor_LeakWater')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Indoor_LeakWater', 'column', 'DeviceUpDateTime'
go

/*==============================================================*/
/* Table: Device_InfraredAlarm                                  */
/*==============================================================*/
create table Device_InfraredAlarm (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   DeviceStatus         int                  null,
   AlarmState           int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_InfraredAlarm') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '红外线报警监测', 
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_InfraredAlarm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_InfraredAlarm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_InfraredAlarm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_InfraredAlarm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_InfraredAlarm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_InfraredAlarm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'DeviceStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备运行状态 1 异常 0正常',
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'DeviceStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_InfraredAlarm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_InfraredAlarm')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_InfraredAlarm', 'column', 'DeviceUpDateTime'
go

/*==============================================================*/
/* Table: Device_Iron_Tower                                     */
/*==============================================================*/
create table Device_Iron_Tower (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   LeftX                nvarchar(50)         null,
   LeftX_AlarmState     int                  null,
   RightY               nvarchar(50)         null,
   RightY_AlarmState    int                  null,
   TempeRature          nvarchar(50)         null,
   AlarmState_TempeRature int                  null,
   DeviceStatus         int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Iron_Tower') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Iron_Tower' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '铁塔倾斜度监测', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LeftX')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'LeftX'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '铁塔X轴',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'LeftX'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LeftX_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'LeftX_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '铁塔X轴报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'LeftX_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RightY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'RightY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '铁塔Y轴',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'RightY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RightY_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'RightY_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '铁塔Y轴报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'RightY_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常温度',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'AlarmState_TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '温度报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'AlarmState_TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'DeviceStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备运行状态1 异常 0正常',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'DeviceStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Iron_Tower')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Iron_Tower', 'column', 'DeviceUpDateTime'
go

alter table Device_Iron_Tower
   add constraint PK_DEVICE_IRON_TOWER primary key (Id)
go

/*==============================================================*/
/* Table: Device_Power_Cable                                    */
/*==============================================================*/
create table Device_Power_Cable (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   TempeRature          nvarchar(50)         null,
   AlarmState_TempeRature int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Power_Cable') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Power_Cable' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '动力电缆表面温度监测', 
   'user', @CurrentUser, 'table', 'Device_Power_Cable'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Power_Cable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Power_Cable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Power_Cable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Power_Cable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Power_Cable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Power_Cable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常温度',
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Power_Cable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AlarmState_TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'AlarmState_TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '温度报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'AlarmState_TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Power_Cable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Power_Cable', 'column', 'DeviceUpDateTime'
go

alter table Device_Power_Cable
   add constraint PK_DEVICE_POWER_CABLE primary key (Id)
go

/*==============================================================*/
/* Table: Device_Switching_State                                */
/*==============================================================*/
create table Device_Switching_State (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   Switching_State      int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Switching_State') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Switching_State' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '设备开关状态', 
   'user', @CurrentUser, 'table', 'Device_Switching_State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Switching_State')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Switching_State')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Switching_State')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Switching_State')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Switching_State')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Switching_State')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Switching_State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'Switching_State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备开关状态 1 开启 0关闭  off/on',
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'Switching_State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Switching_State')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备设置时间',
   'user', @CurrentUser, 'table', 'Device_Switching_State', 'column', 'DeviceUpDateTime'
go

/*==============================================================*/
/* Table: Device_Three_Electric                                 */
/*==============================================================*/
create table Device_Three_Electric (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   A_Voltage            nvarchar(50)         null,
   A_Voltage_AlarmState int                  null,
   B_Voltage            nvarchar(50)         null,
   B_Voltage_AlarmState int                  null,
   C_Voltage            nvarchar(50)         null,
   C_Voltage_AlarmState int                  null,
   A_Current            nvarchar(50)         null,
   A_Current_AlarmState int                  null,
   B_A_Current          nvarchar(50)         null,
   B_Current_AlarmState int                  null,
   C_Current            nvarchar(50)         null,
   C_Current_AlarmState int                  null,
   ZeroCurrent          nvarchar(50)         null,
   ZeroCurrent_AlarmState int                  null,
   TotalPower           nvarchar(50)         null,
   TotalPower_AlarmState int                  null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Device_Three_Electric') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Device_Three_Electric' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '三相电参数实时监测', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'A_Voltage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'A_Voltage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'A相电压',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'A_Voltage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'A_Voltage_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'A_Voltage_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B相电压报警状态1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'A_Voltage_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'B_Voltage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'B_Voltage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B相电压',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'B_Voltage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'B_Voltage_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'B_Voltage_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B相电压报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'B_Voltage_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'C_Voltage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'C_Voltage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'C相电压',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'C_Voltage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'C_Voltage_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'C_Voltage_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'C相电压报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'C_Voltage_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'A_Current')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'A_Current'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'A相电流',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'A_Current'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'A_Current_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'A_Current_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'A相电流报警状态1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'A_Current_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'B_A_Current')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'B_A_Current'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B相电流',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'B_A_Current'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'B_Current_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'B_Current_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B相电流报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'B_Current_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'C_Current')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'C_Current'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'C相电流',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'C_Current'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'C_Current_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'C_Current_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'C相电流报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'C_Current_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ZeroCurrent')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'ZeroCurrent'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '零线电流',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'ZeroCurrent'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ZeroCurrent_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'ZeroCurrent_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '零线电流报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'ZeroCurrent_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalPower')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'TotalPower'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '零线电流',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'TotalPower'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalPower_AlarmState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'TotalPower_AlarmState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '总有功率报警状态 1 报警 0未报交警',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'TotalPower_AlarmState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Device_Three_Electric')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Device_Three_Electric', 'column', 'DeviceUpDateTime'
go

/*==============================================================*/
/* Table: Humidity_Threshold                                    */
/*==============================================================*/
create table Humidity_Threshold (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   Highest_Humidity     nvarchar(50)         null,
   Lowest_Humidity      nvarchar(50)         null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Humidity_Threshold') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Humidity_Threshold' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '湿度阈值设置', 
   'user', @CurrentUser, 'table', 'Humidity_Threshold'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Humidity_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Humidity_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Humidity_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Humidity_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Humidity_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Humidity_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Highest_Humidity')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'Highest_Humidity'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最高湿度',
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'Highest_Humidity'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Humidity_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Lowest_Humidity')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'Lowest_Humidity'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最低湿度',
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'Lowest_Humidity'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Humidity_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '阈值更新时间',
   'user', @CurrentUser, 'table', 'Humidity_Threshold', 'column', 'DeviceUpDateTime'
go

alter table Humidity_Threshold
   add constraint PK_HUMIDITY_THRESHOLD primary key (Id)
go

/*==============================================================*/
/* Table: Iron_Tower_Threshold                                  */
/*==============================================================*/
create table Iron_Tower_Threshold (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   X_Highest            nvarchar(50)         null,
   X_Lowest             nvarchar(50)         null,
   Y_Highest            nvarchar(50)         null,
   Y_Lowest             nvarchar(50)         null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Iron_Tower_Threshold') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '铁塔倾斜度阈值设置', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'X_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'X_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '铁塔X轴最高角度',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'X_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'X_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'X_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '铁塔X轴最低角度',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'X_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Y_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'Y_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '铁塔Y轴最高角度',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'Y_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Y_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'Y_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '铁塔Y轴最低角度',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'Y_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Iron_Tower_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '阈值设置时间',
   'user', @CurrentUser, 'table', 'Iron_Tower_Threshold', 'column', 'DeviceUpDateTime'
go

alter table Iron_Tower_Threshold
   add constraint PK_IRON_TOWER_THRESHOLD primary key (Id)
go

/*==============================================================*/
/* Table: Resistance_Threshold                                  */
/*==============================================================*/
create table Resistance_Threshold (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   Highest_Resistance   nvarchar(50)         null,
   Lowest_Resistance    nvarchar(50)         null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Resistance_Threshold') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Resistance_Threshold' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '电阻阈值设置', 
   'user', @CurrentUser, 'table', 'Resistance_Threshold'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Resistance_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Resistance_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Resistance_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Resistance_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Resistance_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Resistance_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Highest_Resistance')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'Highest_Resistance'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最高电阻',
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'Highest_Resistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Resistance_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Lowest_Resistance')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'Lowest_Resistance'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最低电阻',
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'Lowest_Resistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Resistance_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '阈值设置时间',
   'user', @CurrentUser, 'table', 'Resistance_Threshold', 'column', 'DeviceUpDateTime'
go

alter table Resistance_Threshold
   add constraint PK_RESISTANCE_THRESHOLD primary key (Id)
go

/*==============================================================*/
/* Table: Temperature_Threshold                                 */
/*==============================================================*/
create table Temperature_Threshold (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   Highest_TempeRature  nvarchar(50)         null,
   Lowest_TempeRature   nvarchar(50)         null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Temperature_Threshold') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Temperature_Threshold' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '温度阈值设置', 
   'user', @CurrentUser, 'table', 'Temperature_Threshold'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Temperature_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Temperature_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Temperature_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Temperature_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Temperature_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Temperature_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Highest_TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'Highest_TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最高温度',
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'Highest_TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Temperature_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Lowest_TempeRature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'Lowest_TempeRature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最低温度',
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'Lowest_TempeRature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Temperature_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '阈值设置时间',
   'user', @CurrentUser, 'table', 'Temperature_Threshold', 'column', 'DeviceUpDateTime'
go

alter table Temperature_Threshold
   add constraint PK_TEMPERATURE_THRESHOLD primary key (Id)
go

/*==============================================================*/
/* Table: Three_Electric_Threshold                              */
/*==============================================================*/
create table Three_Electric_Threshold (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   A_Voltage_Highest    nvarchar(50)         null,
   A_Voltage_Lowest     nvarchar(50)         null,
   B_Voltage_Highest    nvarchar(50)         null,
   B_Voltage_Lowest     nvarchar(50)         null,
   C_Voltage_Highest    nvarchar(50)         null,
   C_Voltage_Lowest     nvarchar(50)         null,
   A_Current_Highest    nvarchar(50)         null,
   A_Current_Lowest     nvarchar(50)         null,
   B_Current_Highest    nvarchar(50)         null,
   B_Current_Lowest     nvarchar(50)         null,
   C_Current_Highest    nvarchar(50)         null,
   C_Current_Lowest     nvarchar(50)         null,
   ZeroCurrent_Highest  nvarchar(50)         null,
   ZeroCurrent_Lowest   nvarchar(50)         null,
   TotalPower_Highest   nvarchar(50)         null,
   TotalPower_Lowest    nvarchar(50)         null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Three_Electric_Threshold') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '三相电阈值设置', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'A_Voltage_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'A_Voltage_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'A相电压最高电压',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'A_Voltage_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'A_Voltage_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'A_Voltage_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'A相电压最低电压',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'A_Voltage_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'B_Voltage_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'B_Voltage_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B相电压最高电压',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'B_Voltage_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'B_Voltage_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'B_Voltage_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B相电压最低电压',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'B_Voltage_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'C_Voltage_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'C_Voltage_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'C相电压最高电压',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'C_Voltage_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'C_Voltage_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'C_Voltage_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'C相电压最低电压',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'C_Voltage_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'A_Current_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'A_Current_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'A相电流最高电流',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'A_Current_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'A_Current_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'A_Current_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'A相电流最低电流',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'A_Current_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'B_Current_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'B_Current_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B相电流最高电流',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'B_Current_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'B_Current_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'B_Current_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B相电流最低电流',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'B_Current_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'C_Current_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'C_Current_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'C相电流最高电流',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'C_Current_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'C_Current_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'C_Current_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'C相电流最低电流',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'C_Current_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ZeroCurrent_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'ZeroCurrent_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '零线电流最高电流',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'ZeroCurrent_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ZeroCurrent_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'ZeroCurrent_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '零线电流最低电流',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'ZeroCurrent_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalPower_Highest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'TotalPower_Highest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '总有功率最高',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'TotalPower_Highest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalPower_Lowest')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'TotalPower_Lowest'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '总有功率最低',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'TotalPower_Lowest'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Three_Electric_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '阈值设置时间',
   'user', @CurrentUser, 'table', 'Three_Electric_Threshold', 'column', 'DeviceUpDateTime'
go

/*==============================================================*/
/* Table: Voltage_Threshold                                     */
/*==============================================================*/
create table Voltage_Threshold (
   Id                   nvarchar(50)         not null,
   DeviceNode           nvarchar(50)         null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   Highest_Voltage      nvarchar(50)         null,
   Lowest_Voltage       nvarchar(50)         null,
   DeviceUpDateTime     datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Voltage_Threshold') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Voltage_Threshold' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '电压阈值设置', 
   'user', @CurrentUser, 'table', 'Voltage_Threshold'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Voltage_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Voltage_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceNode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'DeviceNode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'DeviceNode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Voltage_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Voltage_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Voltage_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Voltage_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Highest_Voltage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'Highest_Voltage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最高电压',
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'Highest_Voltage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Voltage_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Lowest_Voltage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'Lowest_Voltage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最低电压',
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'Lowest_Voltage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Voltage_Threshold')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeviceUpDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'DeviceUpDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '阈值设置时间',
   'user', @CurrentUser, 'table', 'Voltage_Threshold', 'column', 'DeviceUpDateTime'
go

alter table Voltage_Threshold
   add constraint PK_VOLTAGE_THRESHOLD primary key (Id)
go

