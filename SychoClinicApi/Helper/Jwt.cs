﻿namespace SychoClinicApi.Helper
{
    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public double Duration { get; set; }
    }
}
