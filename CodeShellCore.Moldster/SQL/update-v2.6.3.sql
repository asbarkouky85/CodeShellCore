alter table PageParameters add UseDefault bit null;
GO
update PageParameters set UseDefault=1;
GO
alter table PageParameters alter column UseDefault bit not null;