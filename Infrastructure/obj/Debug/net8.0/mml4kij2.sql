IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [ApprovalStatus] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    CONSTRAINT [PK_ApprovalStatus] PRIMARY KEY ([Id])
);

CREATE TABLE [ApproverRole] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    CONSTRAINT [PK_ApproverRole] PRIMARY KEY ([Id])
);

CREATE TABLE [Area] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    CONSTRAINT [PK_Area] PRIMARY KEY ([Id])
);

CREATE TABLE [ProjectType] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    CONSTRAINT [PK_ProjectType] PRIMARY KEY ([Id])
);

CREATE TABLE [User] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(25) NOT NULL,
    [Email] varchar(25) NOT NULL,
    [Role] int NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_User_ApproverRole_Role] FOREIGN KEY ([Role]) REFERENCES [ApproverRole] ([Id]) ON DELETE NO ACTION
);

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

CREATE TABLE [ProjectProposals] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [EstimatedAmount] decimal(18,2) NOT NULL,
    [EstimatedDuration] int NOT NULL,
    [CreateAt] datetime2 NOT NULL,
    [Area] int NOT NULL,
    [Type] int NOT NULL,
    [Status] int NOT NULL,
    [CreateBy] int NOT NULL,
    [ProjectProposalAreaId] int NOT NULL,
    [ProjectProposalTypeId] int NOT NULL,
    [ProjectProposalApprovalStatusId] int NOT NULL,
    [ProjectProposalUserId] int NOT NULL,
    CONSTRAINT [PK_ProjectProposals] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectProposals_ApprovalStatus_ProjectProposalApprovalStatusId] FOREIGN KEY ([ProjectProposalApprovalStatusId]) REFERENCES [ApprovalStatus] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectProposals_Area_ProjectProposalAreaId] FOREIGN KEY ([ProjectProposalAreaId]) REFERENCES [Area] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectProposals_ProjectType_ProjectProposalTypeId] FOREIGN KEY ([ProjectProposalTypeId]) REFERENCES [ProjectType] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectProposals_User_ProjectProposalUserId] FOREIGN KEY ([ProjectProposalUserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ProjectApprovalSteps] (
    [Id] bigint NOT NULL IDENTITY,
    [StepOrder] int NOT NULL,
    [DecisionDate] datetime2 NULL,
    [Observations] nvarchar(max) NULL,
    [ProjectProposalId] uniqueidentifier NOT NULL,
    [ApproverUserId] int NULL,
    [ApproverRoleId] int NOT NULL,
    [Status] int NOT NULL,
    [StepUserId] int NULL,
    [StepApprovalStatusId] int NOT NULL,
    CONSTRAINT [PK_ProjectApprovalSteps] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectApprovalSteps_ApprovalStatus_StepApprovalStatusId] FOREIGN KEY ([StepApprovalStatusId]) REFERENCES [ApprovalStatus] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectApprovalSteps_ApproverRole_ApproverRoleId] FOREIGN KEY ([ApproverRoleId]) REFERENCES [ApproverRole] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectApprovalSteps_ProjectProposals_ProjectProposalId] FOREIGN KEY ([ProjectProposalId]) REFERENCES [ProjectProposals] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectApprovalSteps_User_StepUserId] FOREIGN KEY ([StepUserId]) REFERENCES [User] ([Id])
);

CREATE INDEX [IX_ApprovalRule_ApprovalRuleApproverRoleId] ON [ApprovalRule] ([ApprovalRuleApproverRoleId]);

CREATE INDEX [IX_ApprovalRule_ApprovalRuleAreaId] ON [ApprovalRule] ([ApprovalRuleAreaId]);

CREATE INDEX [IX_ApprovalRule_ApprovalRuleTypeId] ON [ApprovalRule] ([ApprovalRuleTypeId]);

CREATE INDEX [IX_ProjectApprovalSteps_ApproverRoleId] ON [ProjectApprovalSteps] ([ApproverRoleId]);

CREATE INDEX [IX_ProjectApprovalSteps_ProjectProposalId] ON [ProjectApprovalSteps] ([ProjectProposalId]);

CREATE INDEX [IX_ProjectApprovalSteps_StepApprovalStatusId] ON [ProjectApprovalSteps] ([StepApprovalStatusId]);

CREATE INDEX [IX_ProjectApprovalSteps_StepUserId] ON [ProjectApprovalSteps] ([StepUserId]);

CREATE INDEX [IX_ProjectProposals_ProjectProposalApprovalStatusId] ON [ProjectProposals] ([ProjectProposalApprovalStatusId]);

CREATE INDEX [IX_ProjectProposals_ProjectProposalAreaId] ON [ProjectProposals] ([ProjectProposalAreaId]);

CREATE INDEX [IX_ProjectProposals_ProjectProposalTypeId] ON [ProjectProposals] ([ProjectProposalTypeId]);

CREATE INDEX [IX_ProjectProposals_ProjectProposalUserId] ON [ProjectProposals] ([ProjectProposalUserId]);

CREATE INDEX [IX_User_Role] ON [User] ([Role]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250422043439_1', N'9.0.4');

COMMIT;
GO

