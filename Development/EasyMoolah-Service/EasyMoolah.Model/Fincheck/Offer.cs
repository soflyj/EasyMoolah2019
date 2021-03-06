﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EasyMoolah.Model.Fincheck
{
    public class OfferRequest
    {
        public int applicationKey { get; set; }        
        public string id_number { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string cell_phone_number { get; set; }
        public string email { get; set; }
        public string intent { get; set; }
        public int intent_id { get; set; }
        public decimal gross_income { get; set; }
        public decimal net_income { get; set; }
        public bool citizen { get; set; } // true / false
        public string debt_review { get; set; } // yes / no
        public bool debt_review_opt { get; set; } // true / false
        public bool popi { get; set; } // true / false //default
        public decimal loan_amount_required { get; set; }
        public int repayment_period { get; set; } // in months
        public int expenses { get; set; }
        public string education { get; set; } // retrieved from the api doc
        public string city { get; set; }
        public int payday { get; set; } // day of the month eg. 25
        public string province { get; set; }
        public bool employed { get; set; } // true / false
        public int employment_period { get; set; }  // eg. 1 year
        public int payment_frequency { get; set; }
        public string bank_name { get; set; }
        public int credit_score { get; set; }
        public string referee_id { get; set; } // yes / no
    }

    public class Userable
    {
        public string partner_type { get; set; }
    }

    public class OfferResponse
    {
        public List<matches> matches { get; set; }
        public string id { get; set; }
    }

    public class matches
    {
        public int id { get; set; }
        public string company_name { get; set; }
        public string company_logo_path { get; set; }
        public string company_logo_url { get; set; }
        public string company_website_url { get; set; }
        public int probability { get; set; }
        public string rank { get; set; }
        public Userable userable { get; set; }
        public live_score live_score { get; set; }
        public bool hasOffers { get; set; }
    }

    public class live_score
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public offers[] offers { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; } //multipleOffers or null if single
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }
    }

    public class offers
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int QuoteId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int OfferId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ProductCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int TermAllowed { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal LoanAmount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal InterestRate { get; set; }
    }
}
