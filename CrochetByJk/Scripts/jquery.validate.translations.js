﻿jQuery.extend(jQuery.validator.messages,
{
    required: "To pole jest wymagane.",
    remote: "Proszę poprawić to pole",
    email: "Proszę podać poprawny adres email.",
    url: "Proszę podać poprawny adres URL.",
    date: "Proszę podać poprawną datę.",
    dateISO: "Proszę podać poprawną datę (ISO).",
    number: "Proszę podać poprawny numer.",
    digits: "Proszę podać tylko cyfry.",
    creditcard: "Proszę podać poprawny numer karty kredytowej.",
    equalTo: "Proszę podać tą samą wartość raz jeszcze.",
    accept: "Proszę podać wartość z poprawnym rozszerzeniem.",
    maxlength: jQuery.validator.format("Proszę podać nie więcej niż {0} znaków."),
    minlength: jQuery.validator.format("Proszę podać więcej niż {0} znaków."),
    rangelength: jQuery.validator.format("Proszę podać wartość z przediału od {0} do {1} znaków."),
    range: jQuery.validator.format("Prośże podać wartość z przedziału od {0} do {1}."),
    max: jQuery.validator.format("Proszę podać wartość mniejszą lub równą {0}."),
    min: jQuery.validator.format("Proszę podać wartość większą lub równą {0}.")
});