CREATE SCHEMA Auth;
GO
-- ADDED TABLES :
CREATE TABLE [TEST2_Conf].Auth.Apps (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.Domains (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.ResourceActions (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.Resources (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.RoleResourceActions (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.RoleResources (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.Roles (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.Tenants (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.UserEntityLinks (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.UserRoles (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].Auth.Users (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Addresses (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.AnesthesiaTypes (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.AssitantTypes (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Attachments (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Cities (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Clinics (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Consumables (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Countries (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Disorders (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Doctors (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.EmployeeClinics (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Employees (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.ExpenseTypes (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.ExternalPeople (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Hospitals (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.InsuranceDeductions (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.InsuranceProviders (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Localizables (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.OperationAssistants (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.OperationConsumables (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.OperationExpenses (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Operations (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.OperationTypes (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Patients (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.ReferralTypes (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Statuses (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.SurgeryPositions (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.SurgeryRooms (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.VisitLogs (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.Visits (Id bigint PRIMARY KEY);
CREATE TABLE [TEST2_Conf].dbo.VisitTypes (Id bigint PRIMARY KEY);