# PhoneBook-Task
Database Contains two tables and one view:
- Contact table contains: contactId(primary key), contactName (Unique), and maxnumber.
- Number table containts: NumberID (primary key), Number, contactId (reference contact table becuase the relation between the two tables is one-to-many).
- ContactNumber View displayed all contacts with its numbers.

Application setup:
this application needs SQL server and the database file to be attached to MS SQL server after that it can be run using Visual Studio.
