/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2020/1/10 11:34:30                           */
/*==============================================================*/


alter table A5NodeOnOff
   drop constraint PK_A5NODEONOFF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('A5NodeOnOff')
            and   type = 'U')
   drop table A5NodeOnOff
go

alter table AANodeOnOff
   drop constraint PK_AANODEONOFF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AANodeOnOff')
            and   type = 'U')
   drop table AANodeOnOff
go

alter table Angel
   drop constraint PK_ANGEL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Angel')
            and   type = 'U')
   drop table Angel
go

alter table Battery
   drop constraint PK_BATTERY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Battery')
            and   type = 'U')
   drop table Battery
go

alter table CO2
   drop constraint PK_CO2
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CO2')
            and   type = 'U')
   drop table CO2
go

alter table GroundResistance
   drop constraint PK_GROUNDRESISTANCE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GroundResistance')
            and   type = 'U')
   drop table GroundResistance
go

alter table NodeTempAndHumidity
   drop constraint PK_NODETEMPANDHUMIDITY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NodeTempAndHumidity')
            and   type = 'U')
   drop table NodeTempAndHumidity
go

alter table NodeTemperature
   drop constraint PK_NODETEMPERATURE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NodeTemperature')
            and   type = 'U')
   drop table NodeTemperature
go

alter table ThreeElec
   drop constraint PK_THREEELEC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ThreeElec')
            and   type = 'U')
   drop table ThreeElec
go

/*==============================================================*/
/* Table: A5NodeOnOff                                           */
/*==============================================================*/
create table A5NodeOnOff (
   Id                   nvarchar(50)         not null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   deviceid             nvarchar(50)         null,
   nodeNumber           nvarchar(50)         null,
   mac                  nvarchar(50)         null,
   onOff                nvarchar(50)         null,
   updateTime           datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('A5NodeOnOff') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'A5NodeOnOff' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '节点开启关闭', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('A5NodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('A5NodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('A5NodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('A5NodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('A5NodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'deviceid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'deviceid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采集设备id',
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'deviceid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('A5NodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nodeNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'nodeNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'nodeNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('A5NodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'mac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'mac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   ' mac',
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'mac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('A5NodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'onOff')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'onOff'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开启关闭',
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'onOff'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('A5NodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'updateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'updateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'A5NodeOnOff', 'column', 'updateTime'
go

alter table A5NodeOnOff
   add constraint PK_A5NODEONOFF primary key (Id)
go

/*==============================================================*/
/* Table: AANodeOnOff                                           */
/*==============================================================*/
create table AANodeOnOff (
   Id                   nvarchar(50)         not null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   deviceid             nvarchar(50)         null,
   nodeNumber           nvarchar(50)         null,
   mac                  nvarchar(50)         null,
   onOff                nvarchar(50)         null,
   updateTime           datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('AANodeOnOff') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'AANodeOnOff' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '节点开启关闭', 
   'user', @CurrentUser, 'table', 'AANodeOnOff'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('AANodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('AANodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('AANodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('AANodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('AANodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'deviceid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'deviceid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采集设备id',
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'deviceid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('AANodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nodeNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'nodeNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'nodeNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('AANodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'mac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'mac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   ' mac',
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'mac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('AANodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'onOff')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'onOff'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开启关闭',
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'onOff'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('AANodeOnOff')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'updateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'updateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'AANodeOnOff', 'column', 'updateTime'
go

alter table AANodeOnOff
   add constraint PK_AANODEONOFF primary key (Id)
go

/*==============================================================*/
/* Table: Angel                                                 */
/*==============================================================*/
create table Angel (
   Id                   nvarchar(50)         not null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   nodeNumber           nvarchar(50)         null,
   deviceid             nvarchar(50)         null,
   mac                  nvarchar(50)         null,
   temperatureOriginal  nvarchar(50)         null,
   temperature          nvarchar(50)         null,
   xAngel               nvarchar(50)         null,
   xAngelOriginal       nvarchar(50)         null,
   yAngel               nvarchar(50)         null,
   yAngelOriginal       nvarchar(50)         null,
   updateTime           datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Angel') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Angel' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '角度', 
   'user', @CurrentUser, 'table', 'Angel'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nodeNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'nodeNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'nodeNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'deviceid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'deviceid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采集设备id',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'deviceid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'mac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'mac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   ' mac',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'mac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'temperatureOriginal')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'temperatureOriginal'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '原始温度',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'temperatureOriginal'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'temperature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'temperature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '温度',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'temperature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'xAngel')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'xAngel'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'X角度',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'xAngel'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'xAngelOriginal')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'xAngelOriginal'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'X原始角度',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'xAngelOriginal'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'yAngel')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'yAngel'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Y角度',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'yAngel'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'yAngelOriginal')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'yAngelOriginal'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Y原始角度',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'yAngelOriginal'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Angel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'updateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Angel', 'column', 'updateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Angel', 'column', 'updateTime'
go

alter table Angel
   add constraint PK_ANGEL primary key (Id)
go

/*==============================================================*/
/* Table: Battery                                               */
/*==============================================================*/
create table Battery (
   Id                   nvarchar(50)         not null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   nodeNumber           nvarchar(50)         null,
   deviceid             nvarchar(50)         null,
   mac                  nvarchar(50)         null,
   voltage              nvarchar(500)        null,
   resistance           nvarchar(500)        null,
   "current"            nvarchar(50)         null,
   temperature          nvarchar(50)         null,
   updateTime           datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Battery') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Battery' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '电池电压', 
   'user', @CurrentUser, 'table', 'Battery'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nodeNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'nodeNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'nodeNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'deviceid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'deviceid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采集设备id',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'deviceid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'mac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'mac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   ' mac',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'mac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'voltage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'voltage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电压',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'voltage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'resistance')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'resistance'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电阻 ',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'resistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'current')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'current'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电流 ',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'current'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'temperature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'temperature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '温度',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'temperature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Battery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'updateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Battery', 'column', 'updateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'Battery', 'column', 'updateTime'
go

alter table Battery
   add constraint PK_BATTERY primary key (Id)
go

/*==============================================================*/
/* Table: CO2                                                   */
/*==============================================================*/
create table CO2 (
   Id                   nvarchar(50)         not null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   nodeNumber           nvarchar(50)         null,
   value                nvarchar(50)         null,
   updateTime           datetime             null,
   deviceid             nvarchar(50)         null,
   mac                  nvarchar(50)         null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('CO2') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'CO2' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '二氧化碳变送器', 
   'user', @CurrentUser, 'table', 'CO2'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CO2')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CO2', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'CO2', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CO2')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CO2', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'CO2', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CO2')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CO2', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'CO2', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CO2')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CO2', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'CO2', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CO2')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nodeNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CO2', 'column', 'nodeNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'CO2', 'column', 'nodeNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CO2')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'value')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CO2', 'column', 'value'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '二氧化碳',
   'user', @CurrentUser, 'table', 'CO2', 'column', 'value'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CO2')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'updateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CO2', 'column', 'updateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'CO2', 'column', 'updateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CO2')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'deviceid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CO2', 'column', 'deviceid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采集设备id',
   'user', @CurrentUser, 'table', 'CO2', 'column', 'deviceid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CO2')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'mac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CO2', 'column', 'mac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   ' mac',
   'user', @CurrentUser, 'table', 'CO2', 'column', 'mac'
go

alter table CO2
   add constraint PK_CO2 primary key (Id)
go

/*==============================================================*/
/* Table: GroundResistance                                      */
/*==============================================================*/
create table GroundResistance (
   Id                   nvarchar(50)         not null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   nodeNumber           nvarchar(50)         null,
   resistance           nvarchar(50)         null,
   updateTime           datetime             null,
   deviceid             nvarchar(50)         null,
   mac                  nvarchar(50)         null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('GroundResistance') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'GroundResistance' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '接地电阻', 
   'user', @CurrentUser, 'table', 'GroundResistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('GroundResistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('GroundResistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('GroundResistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('GroundResistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('GroundResistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nodeNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'nodeNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'nodeNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('GroundResistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'resistance')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'resistance'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电阻',
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'resistance'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('GroundResistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'updateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'updateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'updateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('GroundResistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'deviceid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'deviceid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采集设备id',
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'deviceid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('GroundResistance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'mac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'mac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   ' mac',
   'user', @CurrentUser, 'table', 'GroundResistance', 'column', 'mac'
go

alter table GroundResistance
   add constraint PK_GROUNDRESISTANCE primary key (Id)
go

/*==============================================================*/
/* Table: NodeTempAndHumidity                                   */
/*==============================================================*/
create table NodeTempAndHumidity (
   Id                   nvarchar(50)         not null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   nodeNumber           nvarchar(50)         null,
   temperature          nvarchar(50)         null,
   humidity             nvarchar(50)         null,
   updateTime           datetime             null,
   deviceid             nvarchar(50)         null,
   mac                  nvarchar(50)         null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('NodeTempAndHumidity') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '温湿度', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nodeNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'nodeNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'nodeNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'temperature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'temperature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常温度',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'temperature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'humidity')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'humidity'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常湿度',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'humidity'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'updateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'updateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'updateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'deviceid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'deviceid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采集设备id',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'deviceid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTempAndHumidity')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'mac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'mac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   ' mac',
   'user', @CurrentUser, 'table', 'NodeTempAndHumidity', 'column', 'mac'
go

alter table NodeTempAndHumidity
   add constraint PK_NODETEMPANDHUMIDITY primary key (Id)
go

/*==============================================================*/
/* Table: NodeTemperature                                       */
/*==============================================================*/
create table NodeTemperature (
   Id                   nvarchar(50)         not null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   deviceid             nvarchar(50)         null,
   nodeNumber           nvarchar(50)         null,
   mac                  nvarchar(50)         null,
   temperature          nvarchar(50)         null,
   updateTime           datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('NodeTemperature') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'NodeTemperature' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '节点温度', 
   'user', @CurrentUser, 'table', 'NodeTemperature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTemperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTemperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTemperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTemperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTemperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'deviceid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'deviceid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采集设备id',
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'deviceid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTemperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nodeNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'nodeNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'nodeNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTemperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'mac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'mac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   ' mac',
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'mac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTemperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'temperature')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'temperature'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '正常温度',
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'temperature'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('NodeTemperature')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'updateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'updateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'NodeTemperature', 'column', 'updateTime'
go

alter table NodeTemperature
   add constraint PK_NODETEMPERATURE primary key (Id)
go

/*==============================================================*/
/* Table: ThreeElec                                             */
/*==============================================================*/
create table ThreeElec (
   Id                   nvarchar(50)         not null,
   CreatorId            varchar(50)          null,
   CreatorRealName      nvarchar(50)         null,
   CreateTime           datetime             null,
   nodeNumber           nvarchar(50)         null,
   deviceid             nvarchar(50)         null,
   mac                  nvarchar(50)         null,
   voltage              nvarchar(500)        null,
   zeroCurrent          nvarchar(500)        null,
   "current"            nvarchar(500)        null,
   totalPower           nvarchar(50)         null,
   updateTime           datetime             null
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ThreeElec') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ThreeElec' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '电池电压', 
   'user', @CurrentUser, 'table', 'ThreeElec'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'CreatorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人Id',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'CreatorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorRealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'CreatorRealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人姓名',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'CreatorRealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'CreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'CreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nodeNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'nodeNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备节点号',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'nodeNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'deviceid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'deviceid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采集设备id',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'deviceid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'mac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'mac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   ' mac',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'mac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'voltage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'voltage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电压【A、B、C】',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'voltage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'zeroCurrent')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'zeroCurrent'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '零电流',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'zeroCurrent'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'current')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'current'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电流 ‘【A、B、C】',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'current'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'totalPower')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'totalPower'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '总功率',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'totalPower'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ThreeElec')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'updateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'updateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设备数据采集时间',
   'user', @CurrentUser, 'table', 'ThreeElec', 'column', 'updateTime'
go

alter table ThreeElec
   add constraint PK_THREEELEC primary key (Id)
go

