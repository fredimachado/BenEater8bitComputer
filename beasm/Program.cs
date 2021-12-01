using BenEater8BitComputer.Compiler;

if (args.Length == 0)
{
    Console.WriteLine("Usage: beasm <source-file>");
    return 0;
}

var sourceFile = args[0];

if (!File.Exists(sourceFile))
{
    Console.Error.WriteLine($"Error: File '{sourceFile}' doesn't exist.");
    return 1;
}

var input = File.ReadAllText(sourceFile);
var program = BenEater8BitComputer.Compiler.Program.Parse(input);

if (program.Diagnostics.Any())
{
    foreach (var diagnostic in program.Diagnostics)
    {
        var (lineNumber, column) = program.Text.GetLineNumberAndColumn(diagnostic.Span);
        Console.Error.WriteLine($"Error: {diagnostic.Message} Line: {lineNumber}, Column: {column}.");
    }

    return 1;
}

var assembler = new Assembler(program);
var result = assembler.Emit();

if (!string.IsNullOrWhiteSpace(result.Error))
{
    Console.Error.WriteLine(result.Error);
    return 1;
}

File.WriteAllBytes("a.out", result.Output);

return 0;
