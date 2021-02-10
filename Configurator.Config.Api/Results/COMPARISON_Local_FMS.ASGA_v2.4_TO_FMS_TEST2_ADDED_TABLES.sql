CREATE SCHEMA Asts;
GO
CREATE SCHEMA Auth;
GO
CREATE SCHEMA Legl;
GO
CREATE SCHEMA Main;
GO
CREATE SCHEMA Note;
GO
CREATE SCHEMA Publ;
GO
CREATE SCHEMA Purc;
GO
CREATE SCHEMA Rpt;
GO
CREATE SCHEMA Sals;
GO
CREATE SCHEMA Wfrc;
GO
-- ADDED TABLES :
CREATE TABLE [FMS_TEST2].Asts.AssetsConfig (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Attachments (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Cities (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Contracts (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.CustomFields (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.FolderWorkOrderTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.ItemFolders (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.ItemItemFolders (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Items (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.ItemStates (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Localizables (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Locations (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Parties (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.PartTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.ResourceComponents (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.ResourceTypeFolders (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.ResourceTypePartTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.ResourceTypeResourceTypeFolders (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.ResourceTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Sites (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Specifications (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Statuses (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.StockReservations (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Stocks (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.StockTransactionItems (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.StockTransactions (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.StockTransactionTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Asts.Warehouses (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.DefaultRoles (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.Domains (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.ResourceActions (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.Resources (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.RoleResourceActions (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.RoleResources (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.Roles (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.TenantApps (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.TenantAppUsers (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.TenantDomains (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.Tenants (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.UserParties (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.UserRoles (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Auth.Users (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].dbo.Versions (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.Agents (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.ApprovalPersonTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.ContractResourceTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.Contracts (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.Fines (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.ItemPriorities (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.Localizables (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.Parties (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.RequiredApprovals (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.ResourceTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.Agents (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.Attachments (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.CheckUpFields (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.CheckUpItems (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.CheckUps (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.CheckUpTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.CheckUpValues (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.CustomFields (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.DispatchRuleAgents (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.DispatchRuleConditions (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.DispatchRules (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.EquipmentTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.FaultTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.Localizables (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.MaintenanceConfig (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.Parties (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.PartyWorkOrderTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.ScheduleAgent (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.ScheduleItems (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.Schedules (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.Sites (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.Statuses (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.TaskTemplates (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.WorkOrderActionLogs (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.WorkOrderAgents (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.WorkOrderEquipmentTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.WorkOrderItems (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.WorkOrderParts (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.WorkOrders (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.WorkOrderTasks (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.WorkOrderTeams (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Main.WorkOrderTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Note.NotificationConfigs (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Note.Notifications (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Note.ResourceEvents (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Note.Resources (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Note.UserRoles (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Note.Users (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Publ.ContactInformations (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Publ.FAQs (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Publ.Localizables (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Publ.TermsandConditions (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.Localizables (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.Parties (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.PartTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.PhoneNumbers (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.PriceListItems (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.PriceLists (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.PurchaseOrderItems (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.PurchaseOrders (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.PurchaseRequestItems (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.PurchaseRequests (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.PurchasingConfig (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.ResourceTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.Statuses (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.SupplierActivities (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.SupplierContacts (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.SupplierDevices (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Purc.Suppliers (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Rpt.ReportsTemplates (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.Attachments (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.Cities (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.Contacts (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.Faults (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.Localizables (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.Organizations (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.Parties (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.SalesConfig (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.Sites (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.Statuses (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.WorkRequestActionLogs (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.WorkRequestItemTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.WorkRequests (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Sals.WorkRequestTypes (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.AgentEquipment (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.AgentLocations (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.Agents (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.AgentsTeams (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.Cities (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.Countries (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.Departments (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.JobRoles (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.Localizables (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.Locations (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.Parties (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.Regions (Id bigint PRIMARY KEY);
CREATE TABLE [FMS_TEST2].Wfrc.Teams (Id bigint PRIMARY KEY);
