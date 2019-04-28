create table pm_Role(
id int not null IDENTITY(1,1) primary key,
role_title varchar(255)
);

create table pm_User(
id int not null IDENTITY(1,1) primary key,
firstname varchar(255),
lastname varchar(255),
mobile varchar(255),
email varchar(255),
password varchar(255),
photo varchar(255),
description text,
role_id int foreign key references pm_Role(id),
);



create table pm_project(
id int not null IDENTITY(1,1) primary key,
title varchar(255),
p_description text,
admin_approved int,
project_manger_id int foreign key references pm_User(id),
p_state int,
price int, 
start_date datetime,
end_date datetime
);

create table pm_feedback(
id int not null IDENTITY(1,1) primary key,
evaluator_id int,
evaluated_id int,
rating int,
reason text
);
create table pm_projectTeam(
id int not null IDENTITY(1,1) primary key,
project_id int foreign key references pm_project(id),
member_id int,
postion int,
state int default 0
);
create table pm_personSkill(
id int not null IDENTITY(1,1) primary key,
project_manger_id int foreign key references pm_User(id),
skill varchar(255),
ps_level int
);


create table pm_projectComments
(
id int not null IDENTITY(1,1) primary key,
project_id int foreign key references pm_project(id),
comment varchar(255)
);
