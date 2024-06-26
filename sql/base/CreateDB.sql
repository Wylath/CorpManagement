USE [DBCorpManagement]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [article](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[ref] [varchar](100) NULL,
	[idprovider] [int] NOT NULL,
	[price] [float] NOT NULL,
	[amount] [int] NOT NULL,
	[maxquantity] [int] NULL,
	[idtype] [int] NOT NULL,
	[description] [text] NULL,
	[credit] [int] NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_article] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [article_attribution](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[iduser] [int] NULL,
	[idarticle] [int] NOT NULL,
	[serialnumber] [varchar](150) NULL,
	[specialnumber] [varchar](150) NULL,
	[description] [text] NULL,
	[state] [int] NOT NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_article_attribution] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [fuel](
	[id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_fuel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [gradepoint](
	[id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[totalpoint] [int] NOT NULL,
 CONSTRAINT [PK_gradepoint] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [insurance_vehicle](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[insurancenumber] [int] NOT NULL,
	[idprovider] [int] NOT NULL,
	[effectivedate] [date] NOT NULL,
	[expiredate] [date] NOT NULL,
	[active] [int] NOT NULL,
	[coverage] [text] NOT NULL,
	[price] [float] NULL,
	[description] [text] NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_insurance_vehicle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [invoice](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idinsurance] [int] NULL,
	[idorderarticle] [int] NULL,
	[idservicing] [int] NULL,
	[price] [float] NOT NULL,
	[dateinvoice] [datetime] NOT NULL,
	[description] [text] NULL,
	[datepaid] [datetime] NULL,
	[idtype] [int] NOT NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [invoice_type](
	[id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_invoice_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [order_article](
	[idorder] [int] IDENTITY(1,1) NOT NULL,
	[iduser] [int] NOT NULL,
	[idarticle] [int] NOT NULL,
	[amount] [int] NOT NULL,
	[dateorder] [datetime] NOT NULL,
	[datereceived] [datetime] NULL,
	[status] [int] NOT NULL,
	[description] [text] NULL,
	[idsize] [int] NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_order_article] PRIMARY KEY CLUSTERED 
(
	[idorder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [order_status_type](
	[id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_order_status_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [police_locality](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_police_locality] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [profilelevel](
	[id] [int] NOT NULL,
	[name] [text] NOT NULL,
 CONSTRAINT [PK_grade] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [provider](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[phone] [int] NULL,
	[mail] [varchar](150) NULL,
	[street] [varchar](150) NULL,
	[housenumber] [varchar](50) NULL,
	[postalcode] [varchar](50) NULL,
	[town] [varchar](100) NULL,
	[description] [text] NULL,
	[idtype] [int] NOT NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_provider] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [provider_type](
	[id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_provider_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [servicing](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idvehicle] [int] NOT NULL,
	[dateservicing] [datetime] NOT NULL,
	[price] [float] NOT NULL,
	[idprovider] [int] NOT NULL,
	[idtypeservicing] [int] NOT NULL,
	[km] [int] NOT NULL,
	[description] [text] NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_servicing] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [size_clothing](
	[id] [int] NOT NULL,
	[size] [varchar](10) NOT NULL,
 CONSTRAINT [PK_size_clothing] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [state](
	[id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_state] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [state_article_att](
	[id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_state_article_att] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [status_vehicle](
	[id] [int] NOT NULL,
	[name] [varchar](150) NOT NULL,
 CONSTRAINT [PK_status_vehicle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tires](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](150) NOT NULL,
	[state] [int] NOT NULL,
	[description] [text] NULL,
	[setnumber] [int] NOT NULL,
	[dim1] [varchar](13) NOT NULL,
	[dim2] [varchar](13) NOT NULL,
	[dim3] [varchar](13) NOT NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_tires] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [type_article](
	[id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_type_article] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [type_servicing](
	[id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_type_servicing] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[lastname] [varchar](150) NOT NULL,
	[firstname] [varchar](150) NOT NULL,
	[matricule] [int] NOT NULL,
	[idprofilelevel] [int] NOT NULL,
	[pointarticle] [int] NOT NULL,
	[gradepoint] [int] NOT NULL,
	[status] [int] NOT NULL,
	[lastupdatepoint] [datetime] NOT NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vehicle](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](150) NOT NULL,
	[numberplate] [varchar](50) NOT NULL,
	[idpolicelocality] [int] NOT NULL,
	[saledate] [datetime] NOT NULL,
	[lastcontrol] [datetime] NOT NULL,
	[kmlastconstrol] [int] NOT NULL,
	[nextcontrol] [datetime] NOT NULL,
	[idtires] [int] NULL,
	[fueltype] [int] NOT NULL,
	[vehicletype] [varchar](100) NULL,
	[status] [int] NOT NULL,
	[description] [text] NULL,
	[idinsurance] [int] NULL,
	[createddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifieddate] [datetime] NULL,
	[modifiedby] [int] NULL,
 CONSTRAINT [PK_vehicle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [article] ADD  CONSTRAINT [DF_article_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [article_attribution] ADD  CONSTRAINT [DF_article_attribution_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [insurance_vehicle] ADD  CONSTRAINT [DF_insurance_vehicle_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [insurance_vehicle] ADD  CONSTRAINT [DF_insurance_vehicle_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [invoice] ADD  CONSTRAINT [DF_invoice_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [order_article] ADD  CONSTRAINT [DF_order_article_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [order_article] ADD  CONSTRAINT [DF_order_article_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [police_locality] ADD  CONSTRAINT [DF_police_locality_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [provider] ADD  CONSTRAINT [DF_provider_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [servicing] ADD  CONSTRAINT [DF_servicing_km]  DEFAULT ((0)) FOR [km]
GO
ALTER TABLE [servicing] ADD  CONSTRAINT [DF_servicing_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [tires] ADD  CONSTRAINT [DF_tires_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [user] ADD  CONSTRAINT [DF_user_gradepoint]  DEFAULT ((0)) FOR [gradepoint]
GO
ALTER TABLE [user] ADD  CONSTRAINT [DF_user_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [user] ADD  CONSTRAINT [DF_user_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [vehicle] ADD  CONSTRAINT [DF_vehicle_createddate]  DEFAULT (getdate()) FOR [createddate]
GO
ALTER TABLE [article]  WITH CHECK ADD  CONSTRAINT [FK_article_provider] FOREIGN KEY([idprovider])
REFERENCES [provider] ([id])
GO
ALTER TABLE [article] CHECK CONSTRAINT [FK_article_provider]
GO
ALTER TABLE [article]  WITH CHECK ADD  CONSTRAINT [FK_article_type_article] FOREIGN KEY([idtype])
REFERENCES [type_article] ([id])
GO
ALTER TABLE [article] CHECK CONSTRAINT [FK_article_type_article]
GO
ALTER TABLE [article_attribution]  WITH CHECK ADD  CONSTRAINT [FK_article_attribution_article] FOREIGN KEY([idarticle])
REFERENCES [article] ([id])
GO
ALTER TABLE [article_attribution] CHECK CONSTRAINT [FK_article_attribution_article]
GO
ALTER TABLE [article_attribution]  WITH CHECK ADD  CONSTRAINT [FK_article_attribution_state_article_att] FOREIGN KEY([state])
REFERENCES [state_article_att] ([id])
GO
ALTER TABLE [article_attribution] CHECK CONSTRAINT [FK_article_attribution_state_article_att]
GO
ALTER TABLE [article_attribution]  WITH CHECK ADD  CONSTRAINT [FK_article_attribution_user] FOREIGN KEY([iduser])
REFERENCES [user] ([id])
GO
ALTER TABLE [article_attribution] CHECK CONSTRAINT [FK_article_attribution_user]
GO
ALTER TABLE [insurance_vehicle]  WITH CHECK ADD  CONSTRAINT [FK_insurance_vehicle_provider] FOREIGN KEY([idprovider])
REFERENCES [provider] ([id])
GO
ALTER TABLE [insurance_vehicle] CHECK CONSTRAINT [FK_insurance_vehicle_provider]
GO
ALTER TABLE [invoice]  WITH CHECK ADD  CONSTRAINT [FK_invoice_insurance_vehicle] FOREIGN KEY([idinsurance])
REFERENCES [insurance_vehicle] ([id])
GO
ALTER TABLE [invoice] CHECK CONSTRAINT [FK_invoice_insurance_vehicle]
GO
ALTER TABLE [invoice]  WITH CHECK ADD  CONSTRAINT [FK_invoice_invoice_type] FOREIGN KEY([idtype])
REFERENCES [invoice_type] ([id])
GO
ALTER TABLE [invoice] CHECK CONSTRAINT [FK_invoice_invoice_type]
GO
ALTER TABLE [invoice]  WITH CHECK ADD  CONSTRAINT [FK_invoice_order_article] FOREIGN KEY([idorderarticle])
REFERENCES [order_article] ([idorder])
GO
ALTER TABLE [invoice] CHECK CONSTRAINT [FK_invoice_order_article]
GO
ALTER TABLE [invoice]  WITH CHECK ADD  CONSTRAINT [FK_invoice_servicing1] FOREIGN KEY([idservicing])
REFERENCES [servicing] ([id])
GO
ALTER TABLE [invoice] CHECK CONSTRAINT [FK_invoice_servicing1]
GO
ALTER TABLE [order_article]  WITH CHECK ADD  CONSTRAINT [FK_order_article_article] FOREIGN KEY([idarticle])
REFERENCES [article] ([id])
GO
ALTER TABLE [order_article] CHECK CONSTRAINT [FK_order_article_article]
GO
ALTER TABLE [order_article]  WITH CHECK ADD  CONSTRAINT [FK_order_article_order_status_type] FOREIGN KEY([status])
REFERENCES [order_status_type] ([id])
GO
ALTER TABLE [order_article] CHECK CONSTRAINT [FK_order_article_order_status_type]
GO
ALTER TABLE [order_article]  WITH CHECK ADD  CONSTRAINT [FK_order_article_size_clothing] FOREIGN KEY([idsize])
REFERENCES [size_clothing] ([id])
GO
ALTER TABLE [order_article] CHECK CONSTRAINT [FK_order_article_size_clothing]
GO
ALTER TABLE [order_article]  WITH CHECK ADD  CONSTRAINT [FK_order_article_user] FOREIGN KEY([iduser])
REFERENCES [user] ([id])
GO
ALTER TABLE [order_article] CHECK CONSTRAINT [FK_order_article_user]
GO
ALTER TABLE [provider]  WITH CHECK ADD  CONSTRAINT [FK_provider_provider_type] FOREIGN KEY([idtype])
REFERENCES [provider_type] ([id])
GO
ALTER TABLE [provider] CHECK CONSTRAINT [FK_provider_provider_type]
GO
ALTER TABLE [servicing]  WITH CHECK ADD  CONSTRAINT [FK_servicing_provider] FOREIGN KEY([idprovider])
REFERENCES [provider] ([id])
GO
ALTER TABLE [servicing] CHECK CONSTRAINT [FK_servicing_provider]
GO
ALTER TABLE [servicing]  WITH CHECK ADD  CONSTRAINT [FK_servicing_type_servicing] FOREIGN KEY([idtypeservicing])
REFERENCES [type_servicing] ([id])
GO
ALTER TABLE [servicing] CHECK CONSTRAINT [FK_servicing_type_servicing]
GO
ALTER TABLE [servicing]  WITH CHECK ADD  CONSTRAINT [FK_servicing_vehicle] FOREIGN KEY([idvehicle])
REFERENCES [vehicle] ([id])
GO
ALTER TABLE [servicing] CHECK CONSTRAINT [FK_servicing_vehicle]
GO
ALTER TABLE [tires]  WITH CHECK ADD  CONSTRAINT [FK_tires_state] FOREIGN KEY([state])
REFERENCES [state] ([id])
GO
ALTER TABLE [tires] CHECK CONSTRAINT [FK_tires_state]
GO
ALTER TABLE [user]  WITH CHECK ADD  CONSTRAINT [FK_user_gradepoint] FOREIGN KEY([gradepoint])
REFERENCES [gradepoint] ([id])
GO
ALTER TABLE [user] CHECK CONSTRAINT [FK_user_gradepoint]
GO
ALTER TABLE [user]  WITH CHECK ADD  CONSTRAINT [FK_user_profilelevel] FOREIGN KEY([idprofilelevel])
REFERENCES [profilelevel] ([id])
GO
ALTER TABLE [user] CHECK CONSTRAINT [FK_user_profilelevel]
GO
ALTER TABLE [vehicle]  WITH CHECK ADD  CONSTRAINT [FK_vehicle_fuel] FOREIGN KEY([fueltype])
REFERENCES [fuel] ([id])
GO
ALTER TABLE [vehicle] CHECK CONSTRAINT [FK_vehicle_fuel]
GO
ALTER TABLE [vehicle]  WITH CHECK ADD  CONSTRAINT [FK_vehicle_insurance_vehicle] FOREIGN KEY([idinsurance])
REFERENCES [insurance_vehicle] ([id])
GO
ALTER TABLE [vehicle] CHECK CONSTRAINT [FK_vehicle_insurance_vehicle]
GO
ALTER TABLE [vehicle]  WITH CHECK ADD  CONSTRAINT [FK_vehicle_police_locality] FOREIGN KEY([idpolicelocality])
REFERENCES [police_locality] ([id])
GO
ALTER TABLE [vehicle] CHECK CONSTRAINT [FK_vehicle_police_locality]
GO
ALTER TABLE [vehicle]  WITH CHECK ADD  CONSTRAINT [FK_vehicle_status_vehicle] FOREIGN KEY([status])
REFERENCES [status_vehicle] ([id])
GO
ALTER TABLE [vehicle] CHECK CONSTRAINT [FK_vehicle_status_vehicle]
GO
ALTER TABLE [vehicle]  WITH CHECK ADD  CONSTRAINT [FK_vehicle_tires] FOREIGN KEY([idtires])
REFERENCES [tires] ([id])
GO
ALTER TABLE [vehicle] CHECK CONSTRAINT [FK_vehicle_tires]
GO
