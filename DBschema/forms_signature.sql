create table vplus_dev.forms_signature(
	sign_id INT AUTO_INCREMENT PRIMARY KEY,
    form_id int,
    user_id int,
    role_id int,
    role_name varchar(255),
    is_checked tinyint(1),
    FOREIGN KEY (form_id) REFERENCES forms(id),
    foreign key (role_id) references roles(role_id)
);