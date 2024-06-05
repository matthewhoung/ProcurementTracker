create table vplus_dev.forms_payment(
	payment_id INT AUTO_INCREMENT PRIMARY KEY,
    form_id int,
    payment_total int,
    payment_delta int,
    payment_amount int,
    payment_title_id int,
    payment_tool_id int,
    FOREIGN KEY (form_id) REFERENCES forms(id),
    foreign key (payment_title_id) references pay_types(pay_type_id),
    foreign key (payment_tool_id) references pay_by(pay_by_id)
);