using System;

namespace BookStore.Models.Enums
{
    [Flags]
    public enum BookGenere
    {
        Drama = 1,
        Action = 2,
        Romance = 4,
        Fiction = 8,
        Noval = 16,
        Adventure = 32
    }
}
