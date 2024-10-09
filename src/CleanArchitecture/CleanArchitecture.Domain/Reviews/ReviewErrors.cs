using   CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;


public static class ReviewsErrors
{

    public static readonly Error NotElegible = new(
        "Review.NotElegible",
        "Este review y calificacion para el auto no es elegible por que aun no se completa."
    );
}