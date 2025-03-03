﻿using System.ComponentModel.DataAnnotations;

namespace Store.APIs.DTOs
{
	public class OrderDto
	{
		[Required]
		public string BasketId { get; set; }

		[Required]
		public int DeliveryMethodId { get; set; }

		[Required]
		public AddressDto ShippingAddress { get; set; }
	}
}