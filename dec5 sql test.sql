create  database sql_test_dec5

--customers

alter table customers add status varchar(20) default 'active';


create table Customers(
 CustID INT PRIMARY KEY,
 CustName VARCHAR(100),
 Email VARCHAR(200),
 City VARCHAR(100)
)

INSERT INTO Customers (CustID, CustName, Email, City) VALUES
(1, 'Amit Sharma', 'amit.sharma@gmail.com', 'Mumbai'),
(2, 'Ravi Kumar', 'ravi.kumar@yahoo.com', 'Delhi'),
(3, 'Priya Singh', 'priya.singh@gmail.com', 'Pune'),
(4, 'John Mathew', 'john.mathew@hotmail.com', 'Bangalore'),
(5, 'Sara Thomas', 'sara.thomas@gmail.com', 'Kochi'),
(6, 'Nidhi Jain', 'nidhi.jain@gmail.com', NULL);

select * from Customers


--products

create table Products(
 ProductID INT PRIMARY KEY,
 ProductName VARCHAR(100),
 Price DECIMAL(10,2),
 Stock INT CHECK(Stock >= 0)
)

INSERT INTO Products (ProductID, ProductName, Price, Stock) VALUES
(101, 'Laptop Pro 14', 75000, 15),
(102, 'Laptop Air 13', 55000, 8),
(103, 'Wireless Mouse', 800, 50),
(104, 'Mechanical Keyboard', 3000, 20),
(105, 'USB-C Charger', 1200, 5),
(106, '27-inch Monitor', 18000, 10),
(107, 'Pen Drive 64GB', 600, 80);

select * from products

--orders

create table Orders(
 OrderID INT PRIMARY KEY,
 CustID INT FOREIGN KEY REFERENCES Customers(CustID),
 OrderDate DATE,
 Status VARCHAR(20)
)

INSERT INTO Orders (OrderID, CustID, OrderDate, Status) VALUES
(5001, 1, '2025-01-05', 'Pending'),
(5002, 2, '2025-01-10', 'Completed'),
(5003, 1, '2025-01-20', 'Completed'),
(5004, 3, '2025-02-01', 'Pending'),
(5005, 4, '2025-02-15', 'Completed'),
(5006, 5, '2025-02-18', 'Pending');

INSERT INTO Orders (OrderID, CustID, OrderDate, Status) VALUES
(5007, 5, '2025-11-18', 'Pending');


select * from orders

--orders details

create table OrderDetails(
 DetailID INT PRIMARY KEY,
 OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
 ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
 Qty INT CHECK(Qty > 0)
)

INSERT INTO OrderDetails (DetailID, OrderID, ProductID, Qty) VALUES
(9001, 5001, 101, 1),
(9002, 5001, 103, 2),
 
(9003, 5002, 104, 1),
(9004, 5002, 103, 1),
 
(9005, 5003, 102, 1),
(9006, 5003, 105, 1),
(9007, 5003, 103, 3),
 
(9008, 5004, 106, 1),
 
(9009, 5005, 107, 4),
(9010, 5005, 104, 1),
 
(9011, 5006, 101, 1),
(9012, 5006, 107, 2);

select * from OrderDetails

--payments

create table Payments(
 PaymentID INT PRIMARY KEY,
 OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
 Amount DECIMAL(10,2),
 PaymentDate DATE
)

INSERT INTO Payments (PaymentID, OrderID, Amount, PaymentDate) VALUES
(7001, 5002, 3300, '2025-01-11'),
(7002, 5003, 62000, '2025-01-22'),
(7003, 5005, 4500, '2025-02-16');

select * from Payments


--price history
create table pricehistory
(
    historyid int identity(1,1) primary key,
    productid int,
    oldprice decimal(10,2),
    changedate datetime default getdate()
);

--paymentaudit
create table paymentaudit
(
    auditid int identity(1,1) primary key,
    paymentid int,
    oldamount decimal(10,2),
    newamount decimal(10,2),
    changedate datetime default getdate()
);


--=======================================================================================================================================================================

-- sql queries

--Q1. List customers who placed an order in the last 30 days.
--(Use joins)

select distinct c.custid, c.custname from customers c join orders o on c.custid = o.custid where o.orderdate >= dateadd(day, -30, getdate());


--Q2. Display top 3 products that generated the highest total sales amount.
--(Use aggregate + joins)

select top 3 p.productid, p.productname, sum(od.qty * p.price) as totalsales from orderdetails od join products p on od.productid = p.productid group by p.productid, p.productname order by totalsales desc;


--Q3. For each city, show number of customers and total order count.

select c.city, count(distinct c.custid) as total_customers, count(o.orderid) as total_orders from customers c left join orders o on c.custid = o.custid group by c.city;



--Q4. Retrieve orders that contain more than 2 different products.

select o.orderid, count(distinct od.productid) as product_count from orders o join orderdetails od on o.orderid = od.orderid group by o.orderid having count(distinct od.productid) > 2;


--Q5. Show orders where total payable amount is greater than 10,000.
--(Hint: SUM(Qty * Price))

select o.orderid, sum(od.qty * p.price) as total_amount from orders o join orderdetails od on o.orderid = od.orderid join products p on od.productid = p.productid group by o.orderid having sum(od.qty * p.price) > 10000;


--Q6. List customers who ordered the same product more than once.

select c.custid, c.custname, od.productid, count(*) as order_count from customers c join orders o on c.custid = o.custid join orderdetails od on o.orderid = od.orderid group by c.custid, c.custname, od.productid having count(*) > 1;

--Q7. Display employee-wise order processing details
--(Assume Orders table has EmployeeID column--)

select o.employeeid, count(o.orderid) as totalorders, sum(od.qty * p.price) as totalsales from orders o join orderdetails od on o.orderid = od.orderid join products p on od.productid = p.productid group by o.employeeid;

--=============================================================================================================================================================================================================================================


--vewis

--Views
--1. Create a view vw_LowStockProducts
--Show only products with stock < 5.
--View should be WITH SCHEMABINDING and Encrypted

create view vw_lowstockproducts
with schemabinding, encryption
as
select productid, productname, price, stock
from dbo.products
where stock <5;


--==========================================================================================================================================================================
--Functions
--1. Create a table-valued function: fn_GetCustomerOrderHistory(@CustID)
--Return: OrderID, OrderDate, TotalAmount.


create function fn_getcustomerorderhistory(@custid int)
returns @order_amount_calculate table
(
    orderid int,
    orderdate date,
    total_order_amount decimal(10,2)
)
as
begin
    insert into @order_amount_calculate (orderid, orderdate, total_order_amount)
    select 
        o.orderid,
        o.orderdate,
        sum(od.qty * p.price) as total_order_amount
    from orders o
    join orderdetails od on o.orderid = od.orderid
    join products p on od.productid = p.productid
    where o.custid = @custid
    group by o.orderid, o.orderdate;

    return;
end;

select * from fn_getcustomerorderhistory(2);

--2. Create a function fn_GetCustomerLevel(@CustID)
--Logic:
--• Total purchase > 1,00,000 → "Platinum"
--• 50,000–1,00,000 → "Gold"
--• Else → "Silver"



create function fn_getcustomerlevel(@custid int)
returns varchar(20)
as
begin
    declare @totalpurchase decimal(18,2);
    declare @cust_level varchar(20);

    select @totalpurchase = sum(od.qty * p.price)
    from orders o
    join orderdetails od on o.orderid = od.orderid
    join products p on od.productid = p.productid
    where o.custid = @custid;

    

    if @totalpurchase > 100000
        set @cust_level = 'platinum';
    else if @totalpurchase >= 50000 and @totalpurchase <= 100000
        set @cust_level = 'gold';
    else
        set @cust_level = 'silver';

    return @cust_level;
end;


select dbo.fn_getcustomerlevel(2);

--======================================================================================================

--procedures


--1. Create a stored procedure to update product price
--Rules:
--• Old price must be logged in a PriceHistory table
--• New price must be > 0
--• If invalid, throw custom error.

create procedure sp_updateproductprice
    @productid int,
    @newprice decimal(10,2)
as
begin
    set nocount on;

    if @newprice <= 0
    begin
        throw 50001, 'new price must be greater than zero', 1;
    end;

    declare @oldprice decimal(10,2);

    select @oldprice = price
    from products
    where productid = @productid;

    if @oldprice is null
    begin
        throw 50002, 'invalid product id', 1;
    end;

    insert into pricehistory (productid, oldprice)
    values (@productid, @oldprice);

    update products
    set price = @newprice
    where productid = @productid;
end;


exec sp_updateproductprice 101, 80000;

select * from pricehistory

--2. Create a procedure sp_SearchOrders
--Search orders by:
--• Customer Name
--• City
--• Product Name
--• Date range
--(Any parameter can be NULL → Dynamic WHERE)


create procedure sp_SearchOrders
@customername varchar(100) = null,
@city varchar(50) = null,
@productname varchar(100) = null,
@fromdate date = null,
@todate date = null
as
begin

    select o.orderid, o.orderdate, c.custname, c.city,
    p.productname, od.qty
    from orders o
    join customers c on o.custid = c.custid
    join orderdetails od on o.orderid = od.orderid
    join products p on p.productid = od.productid
    where
        (c.custname like '%' + @customername + '%' ) 

        and (c.city = @city  or )

        and (o.orderdate >= @fromdate or @fromdate)

end


--==================================================================================================================

--Triggers

--1. Create a trigger on Products
--Prevent deletion of a product if it is part of any OrderDetails.


create trigger trg_prevent_product_delete
on products
for delete
as
begin
    if exists (
        select 1
        from orderdetails od
        join deleted d on od.productid = d.productid
    )
    begin
        rollback transaction;
        throw 51000, 'cannot delete product because it is used in orderdetails', 1;
    end;
end;


delete from products where productid = 101;
select * from products


--2. Create an AFTER UPDATE trigger on Payments
--Log old and new payment values into a PaymentAudit table.

create trigger trg_payment_update_audit
on payments
after update
as
begin
    insert into paymentaudit (paymentid, oldamount, newamount)
    select 
        d.paymentid,
        d.amount as oldamount,
        i.amount as newamount
    from deleted d
    join inserted i on d.paymentid = i.paymentid;
end;


update payments set amount = 8888.88 where paymentid = 7001;
select * from paymentaudit;


--3. Create an INSTEAD OF DELETE trigger on Customers
--Logic:
--• If customer has orders → mark status as “Inactive” instead of deleting
--• If no orders → allow deletion

create trigger t3
on customers
instead of delete
as
begin
    update c
    set c.status = 'Inactive'
    from customers c
    inner join deleted d on c.custid = d.custid
    where exists (
        select 1 from orders o where o.custid = d.custid
    );

    -- Delete customer if they have no orders
    delete c
    from customers c
    inner join deleted d on c.custid = d.custid
    where not exists (
        select 1 from orders o where o.custid = d.custid
    );
end;

delete from customers where custid = 1;

delete from customers where custid = 6;
select * from customers where custid = 1;

