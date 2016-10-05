using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Mvc;

namespace QualityCaps.Models
{
    [Bind(Exclude = "OrderId")]
    public partial class Order
    {

        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(40)]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        public string Phone { get; set; }

        public bool SaveInfo { get; set; }


        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public string Status { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

       

        public string ToString(Order order)
        {
            StringBuilder bob = new StringBuilder();

            bob.Append("<p>Order Information for Order: "+ order.OrderId +"<br>Placed at: " + order.OrderDate +"</p>").AppendLine();
            bob.Append("<p>Name: " + order.FirstName + " " + order.LastName + "<br>");
            bob.Append("Address: " + order.Address + " " + order.City + " " + order.State + " " + order.PostalCode + "<br>");
            bob.Append("Contact: " + order.Email + "     " + order.Phone + "</p>");

            bob.Append("<br>").AppendLine();
            bob.Append("<Table>").AppendLine();
            // Display header 
            string header = "<tr> <th>Item Name</th>" + "<th>Quantity</th>" + "<th>Price</th> <th></th> </tr>";
            bob.Append(header).AppendLine();

            String output = String.Empty;
            try
            {
                foreach (var item in order.OrderDetails)
                {
                    bob.Append("<tr>");
                    output = "<td>" + item.Item.Name + "</td>" + "<td>" + item.Quantity + "</td>" + "<td>" + item.Quantity * item.UnitPrice + "</td>";
                    bob.Append(output).AppendLine();
                    Console.WriteLine(output);
                    bob.Append("</tr>");
                }
            }
            catch (Exception ex)
            {
                output = "No items ordered.";
            }
            bob.Append("</Table>");
            bob.Append("<b>");
            // Display footer 
            string footer = $"{"Total",-12}{order.Total,12}\n";
            bob.Append(footer).AppendLine();
            bob.Append("</b>");

            return bob.ToString();
        }
    }
}