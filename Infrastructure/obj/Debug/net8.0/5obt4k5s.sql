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
    [ApprovalRoleId] int NOT NULL,
    [ApprovalRuleAreaId] int NULL,
    [ApprovalRuleTypeId] int NULL,
    [ApprovalRuleApproverRoleId] int NOT NULL,
    CONSTRAINT [PK_ApprovalRule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApprovalRule_ApproverRole_ApprovalRuleApproverRoleId] FOREIGN KEY ([ApprovalRuleApproverRoleId]) REFERENCES [ApproverRole] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ApprovalRule_Area_ApprovalRuleAreaId] FOREIGN KEY ([ApprovalRuleAreaId]) REFERENCES [Area] ([Id]),
    CONSTRAINT [FK_ApprovalRule_ProjectType_ApprovalRuleTypeId] FOREIGN KEY ([ApprovalRuleTypeId]) REFERENCES [ProjectType] ([Id])
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


CREATE INDEX [IX_ApprovalRule_ApprovalRuleApproverRoleId] ON [ApprovalRule] ([ApprovalRuleApproverRoleId]);
GO


CREATE INDEX [IX_ApprovalRule_ApprovalRuleAreaId] ON [ApprovalRule] ([ApprovalRuleAreaId]);
GO


CREATE INDEX [IX_ApprovalRule_ApprovalRuleTypeId] ON [ApprovalRule] ([ApprovalRuleTypeId]);
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


