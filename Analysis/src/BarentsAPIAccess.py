import requests

# # Request a token from the BarentsWatch API (lasts 1 hour, can save output and comment to run iteratively)
# url = "https://id.barentswatch.no/connect/token"
# headers = {
#     "Content-Type": "application/x-www-form-urlencoded"
# }
# data = {
#     "client_id": "russell.b.primeau@ntnu.no:wavesearcher",
#     "scope": "api",
#     "client_secret": "ArY^tztthL5r5arS",
#     "grant_type": "client_credentials"
# }
#
# response = requests.post(url, headers=headers, data=data)
#
# if response.status_code == 200:
#     token = response.json().get("access_token")
#     print("Token retrieved successfully:", token)
# else:
#     print("Failed to retrieve token:", response.status_code)

token = 'eyJhbGciOiJSUzI1NiIsImtpZCI6IjM0QjdCRENCNDcwMkJBQjAxMDExNjRCRTZGNDM1RkU3IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2lkLmJhcmVudHN3YXRjaC5ubyIsIm5iZiI6MTc0MjM3Mzc0NSwiaWF0IjoxNzQyMzczNzQ1LCJleHAiOjE3NDIzNzczNDUsImF1ZCI6ImFwaSIsInNjb3BlIjpbImFwaSJdLCJjbGllbnRfaWQiOiJydXNzZWxsLmIucHJpbWVhdUBudG51Lm5vOndhdmVzZWFyY2hlciJ9.LKDDPKfaEEKb9KfUSH9Cteh2IQpHTJkj0R-lXrd63Zonpw9WKNAhwtRUFS7WU3IQ7_XpmvZqPyx3aAQ3TE9favbkbpDV56NmdHfu6qeDF6YMJjPwg1PC4gn9Pn0sa4h8_UjCYutKgo0kNfB_jlMOVZ6PSAqeunpXlbK_ehMrOUyCjZmI5kyAIiwR38rxdQByZ8Fkry5PiiMvbX5cr0ABg-s74jJVVeewp_v8tikJSVD7cV6hoHBsQoFrMq_DcXWn_DXIt5L2Z2j8PvWjwX6KIiTYlMcQ9r8QKGpjqifZqCRtPwVlU_8iggn16m9ADbsjK8kReSSCaYpvpZT-l1lqtA'

# Then try to make an API request:
# api_url = "https://www.barentswatch.no/bwapi/v1/geodata/waveforecast/available/all"  # available times for wave forecast
api_url = "https://www.barentswatch.no/bwapi/v1/geodata/waveforecast/pointforecasts?time=2025-03-19T12%3A00%3A00Z"  # wave points for all areas
api_headers = {
    "Authorization": f"Bearer {token}"
}

api_response = requests.get(api_url, headers=api_headers)

if api_response.status_code == 200:
    print("Data retrieved successfully!")
    content_type = api_response.headers.get('Content-Type')
    print("Content-Type:", content_type)
    print(api_response.json())
else:
    print("Failed to retrieve data:", api_response.status_code)