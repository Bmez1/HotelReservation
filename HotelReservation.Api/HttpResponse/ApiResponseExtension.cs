using Common;

namespace HotelReservation.Api.HttpResponse;

public static class ApiResponseExtension
{
    public static IResult ToHttpResponse<T>(this Result<T> result) 
    {
        return result.IsSuccess ? Results.Ok(ApiResponseSuccessful<T>.Create(result.Value, result.TotalData)) : 
            CustomResults.Problem(result);
    }
}
