create table vplus_dev.forms_department(
	formdepartment_id INT AUTO_INCREMENT PRIMARY KEY,
    form_id int,
    department_id int,
    FOREIGN KEY (form_id) REFERENCES forms(id),
    foreign key (department_id) references departments(department_id)
);