using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Domain.Aggregatеs;
using MediatR;

namespace HotelManagementSystem.Application.Features;

public record ApplyDiscountCommand(double hotelStarRating, decimal roomPrice): IRequest<ApplyDiscountResult>
{
    
}

public record ApplyDiscountResult(decimal discount, decimal bookingPrice);

public class ApplyDiscountCommandHandler : IRequestHandler<ApplyDiscountCommand, ApplyDiscountResult>
{
    private readonly ILogger<ApplyDiscountCommandHandler> _logger;
    
    public ApplyDiscountCommandHandler(ILogger<ApplyDiscountCommandHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task<ApplyDiscountResult> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
    {
        double hotelStarRating = request.hotelStarRating;
        decimal basePrice = request.roomPrice; 
        decimal discount = (decimal)(hotelStarRating * 5.0);
        decimal bookingPrice = basePrice * (discount / 100);
        _logger.LogInformation("Applying discount of {Discount}% for hotel star rating {HotelStarRating}", discount, hotelStarRating);
        return new ApplyDiscountResult(discount, bookingPrice);
    }
}