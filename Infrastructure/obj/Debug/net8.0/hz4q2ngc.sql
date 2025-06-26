CREATE TABLE [ApprovalStatus] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    CONSTRAINT [PK_ApprovalStatus] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [ApproverRole] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    CONSTRAINT [PK_ApproverRole] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Area] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    CONSTRAINT [PK_Area] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [ProjectType] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    CONSTRAINT [PK_ProjectType] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [User] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    [Email] varchar(25) NOT NULL,
    [Role] int NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_User_ApproverRole_Role] FOREIGN KEY ([Role]) REFERENCES [ApproverRole] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [ApprovalRule] (
    [Id] bigint NOT NULL IDENTITY,
    [MinAmount] decimal(18,2) NOT NULL,
    [MaxAmount] decimal(18,2) NOT NULL,
    [StepOrder] int NOT NULL,
    [Area] int NULL,
    [Type] int NULL,
    [ApproverRoleId] int NOT NULL,
    CONSTRAINT [PK_ApprovalRule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApprovalRule_ApproverRole_ApproverRoleId] FOREIGN KEY ([ApproverRoleId]) REFERENCES [ApproverRole] ([Id]),
    CONSTRAINT [FK_ApprovalRule_Area_Area] FOREIGN KEY ([Area]) REFERENCES [Area] ([Id]),
    CONSTRAINT [FK_ApprovalRule_ProjectType_Type] FOREIGN KEY ([Type]) REFERENCES [ProjectType] ([Id])
);
GO


CREATE TABLE [ProjectProposal] (
    [Id] uniqueidentifier NOT NULL,
    [Title] varchar(255) NOT NULL,
    [Description] varchar(max) NOT NULL,
    [EstimatedAmount] decimal(18,2) NOT NULL,
    [EstimatedDuration] int NOT NULL,
    [CreateAt] datetime2 NOT NULL,
    [Area] int NOT NULL,
    [Type] int NOT NULL,
    [Status] int NOT NULL,
    [CreateBy] int NOT NULL,
    CONSTRAINT [PK_ProjectProposal] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectProposal_ApprovalStatus_Status] FOREIGN KEY ([Status]) REFERENCES [ApprovalStatus] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectProposal_Area_Area] FOREIGN KEY ([Area]) REFERENCES [Area] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectProposal_ProjectType_Type] FOREIGN KEY ([Type]) REFERENCES [ProjectType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectProposal_User_CreateBy] FOREIGN KEY ([CreateBy]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [ProjectApprovalStep] (
    [Id] bigint NOT NULL IDENTITY,
    [StepOrder] int NOT NULL,
    [DecisionDate] datetime2 NULL,
    [Observations] varchar(max) NULL,
    [ProjectProposalId] uniqueidentifier NOT NULL,
    [ApproverUserId] int NULL,
    [ApproverRoleId] int NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_ProjectApprovalStep] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectApprovalStep_ApprovalStatus_Status] FOREIGN KEY ([Status]) REFERENCES [ApprovalStatus] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectApprovalStep_ApproverRole_ApproverRoleId] FOREIGN KEY ([ApproverRoleId]) REFERENCES [ApproverRole] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectApprovalStep_ProjectProposal_ProjectProposalId] FOREIGN KEY ([ProjectProposalId]) REFERENCES [ProjectProposal] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectApprovalStep_User_ApproverUserId] FOREIGN KEY ([ApproverUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ApprovalStatus]'))
    SET IDENTITY_INSERT [ApprovalStatus] ON;
INSERT INTO [ApprovalStatus] ([Id], [Name])
VALUES (1, 'Pending'),
(2, 'Approved'),
(3, 'Rejected'),
(4, 'Observed');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ApprovalStatus]'))
    SET IDENTITY_INSERT [ApprovalStatus] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ApproverRole]'))
    SET IDENTITY_INSERT [ApproverRole] ON;
INSERT INTO [ApproverRole] ([Id], [Name])
VALUES (1, 'Líder de Área'),
(2, 'Gerente'),
(3, 'Director'),
(4, 'Comité Técnico');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ApproverRole]'))
    SET IDENTITY_INSERT [ApproverRole] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Area]'))
    SET IDENTITY_INSERT [Area] ON;
INSERT INTO [Area] ([Id], [Name])
VALUES (1, 'Finanzas'),
(2, 'Tecnología'),
(3, 'Recursos Humanos'),
(4, 'Operaciones');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Area]'))
    SET IDENTITY_INSERT [Area] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ProjectType]'))
    SET IDENTITY_INSERT [ProjectType] ON;
INSERT INTO [ProjectType] ([Id], [Name])
VALUES (1, 'Mejora de Procesos'),
(2, 'Innovación y Desarrollo'),
(3, 'Infraestructura'),
(4, 'Capacitación Interna');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ProjectType]'))
    SET IDENTITY_INSERT [ProjectType] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApproverRoleId', N'Area', N'MaxAmount', N'MinAmount', N'StepOrder', N'Type') AND [object_id] = OBJECT_ID(N'[ApprovalRule]'))
    SET IDENTITY_INSERT [ApprovalRule] ON;
INSERT INTO [ApprovalRule] ([Id], [ApproverRoleId], [Area], [MaxAmount], [MinAmount], [StepOrder], [Type])
VALUES (CAST(1 AS bigint), 1, NULL, 100000.0, 0.0, 1, NULL),
(CAST(2 AS bigint), 2, NULL, 20000.0, 5000.0, 2, NULL),
(CAST(3 AS bigint), 2, 2, 20000.0, 0.0, 1, 2),
(CAST(4 AS bigint), 3, NULL, 0.0, 20000.0, 3, NULL),
(CAST(5 AS bigint), 2, 1, 0.0, 5000.0, 2, 1),
(CAST(6 AS bigint), 1, NULL, 10000.0, 0.0, 1, 2),
(CAST(7 AS bigint), 4, 2, 10000.0, 0.0, 1, 1),
(CAST(8 AS bigint), 2, 2, 30000.0, 10000.0, 2, NULL),
(CAST(9 AS bigint), 3, 3, 0.0, 30000.0, 2, NULL),
(CAST(10 AS bigint), 4, NULL, 50000.0, 0.0, 1, 4);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApproverRoleId', N'Area', N'MaxAmount', N'MinAmount', N'StepOrder', N'Type') AND [object_id] = OBJECT_ID(N'[ApprovalRule]'))
    SET IDENTITY_INSERT [ApprovalRule] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name', N'Role') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] ON;
INSERT INTO [User] ([Id], [Email], [Name], [Role])
VALUES (1, 'jferreyra@unaj.com', 'José Ferreyra', 2),
(2, 'alucero@unaj.com', 'Ana Lucero', 1),
(3, 'gmolinas@unaj.com', 'Gonzalo Molinas', 2),
(4, 'lolivera@unaj.com', 'Lucas Olivera', 3),
(5, 'dfagundez@unaj.com', 'Danilo Fagundez', 4),
(6, 'ggalli@unaj.com', 'Gabriel Galli', 4);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name', N'Role') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] OFF;
GO


CREATE INDEX [IX_ApprovalRule_ApproverRoleId] ON [ApprovalRule] ([ApproverRoleId]);
GO


CREATE INDEX [IX_ApprovalRule_Area] ON [ApprovalRule] ([Area]);
GO


CREATE INDEX [IX_ApprovalRule_Type] ON [ApprovalRule] ([Type]);
GO


CREATE INDEX [IX_ProjectApprovalStep_ApproverRoleId] ON [ProjectApprovalStep] ([ApproverRoleId]);
GO


CREATE INDEX [IX_ProjectApprovalStep_ApproverUserId] ON [ProjectApprovalStep] ([ApproverUserId]);
GO


CREATE INDEX [IX_ProjectApprovalStep_ProjectProposalId] ON [ProjectApprovalStep] ([ProjectProposalId]);
GO


CREATE INDEX [IX_ProjectApprovalStep_Status] ON [ProjectApprovalStep] ([Status]);
GO


CREATE INDEX [IX_ProjectProposal_Area] ON [ProjectProposal] ([Area]);
GO


CREATE INDEX [IX_ProjectProposal_CreateBy] ON [ProjectProposal] ([CreateBy]);
GO


CREATE INDEX [IX_ProjectProposal_Status] ON [ProjectProposal] ([Status]);
GO


CREATE INDEX [IX_ProjectProposal_Type] ON [ProjectProposal] ([Type]);
GO


CREATE INDEX [IX_User_Role] ON [User] ([Role]);
GO


