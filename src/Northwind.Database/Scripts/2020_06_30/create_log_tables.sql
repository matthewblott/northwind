BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Log" (
	"Timestamp"	TEXT,
	"Loglevel"	TEXT,
	"Logger"	TEXT,
	"Callsite"	TEXT,
	"Url"	TEXT,
	"Controller"	TEXT,
	"Action"	TEXT,
	"Message"	TEXT
);
CREATE VIEW LogView as

select Timestamp, Loglevel, 
Message,
substr(Message, instr(Message, 'CommandTimeout=''30'']') + length('CommandTimeout=''30'']') + 1)
as Sql
from Log 
where Logger like '%Command'
and instr(Message, 'CommandTimeout=''30'']') > 0;
COMMIT;
