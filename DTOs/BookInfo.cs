public readonly record struct BookCreateInfo(
    string Author,
    string Title,
    string Description
);

public readonly record struct BookUpdateInfo(
    int Id,
    string Author,
    string Title,
    string Description
);

public readonly record struct BookGetInfo(
    int Id,
    string Author,
    string Title,
    string Description
);
