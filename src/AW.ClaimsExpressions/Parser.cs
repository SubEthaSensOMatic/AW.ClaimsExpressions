using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AW.ClaimsExpressions
{
    public abstract record Node();

    public record EqualsNode(Token Claim, Token Value) : Node;

    public record GreaterNode(Token Claim, Token Value) : Node;

    public record GreaterOrEqualNode(Token Claim, Token Value) : Node;

    public record LessNode(Token Claim, Token Value) : Node;

    public record LessOrEqualNode(Token Claim, Token Value) : Node;

    public record ContainsNode(Token Claim, Token Value) : Node;

    public record StartsWithNode(Token Claim, Token Value) : Node;

    public record EndsWithNode(Token Claim, Token Value) : Node;

    public record ExistsNode(Token Claim) : Node;

    public record NotNode(Node Negated) : Node;

    public record AndNode(Node Left, Node Right) : Node;

    public record OrNode(Node Left, Node Right) : Node;

    /// <summary>
    /// Rekursiver tiefen Parser
    /// 
    /// Expression      -> OrExpression
    /// OrExpression    -> AndExpression ('or' AndExpression)*
    /// AndExpression   -> NotExpression ('and' NotExpression)*
    /// NotExpression   -> 'not' NotExpression | Primary
    /// Claim           -> '[' text ']'
    /// String          -> '\'' text '\''
    /// Integer         -> [+-]?[0-9]+
    /// Float           -> [+-]?([0-9]*[.])?[0-9]+
    /// Primary         -> 'exists' Claim 
    ///                  | Claim '=' String | Integer | Float
    ///                  | Claim '>' String | Integer | Float
    ///                  | Claim '<' String | Integer | Float
    ///                  | Claim '>=' String | Integer | Float
    ///                  | Claim '<=' String | Integer | Float
    ///                  | Claim 'contains' String 
    ///                  | Claim 'startsWith' String 
    ///                  | Claim 'endsWith' String 
    ///                  | '(' Expression ')'
    /// </summary>
    public class Parser
    {
        private Token? Current => _currentPosition < _tokens.Count
            ? _tokens[_currentPosition]
            : null;

        private readonly IReadOnlyList<Token> _tokens;
        private int _currentPosition;

        public Parser(IReadOnlyList<Token> tokens)
        {
            ArgumentNullException.ThrowIfNull(tokens);
            ArgumentOutOfRangeException.ThrowIfZero(tokens.Count);
            _tokens = tokens;
            _currentPosition = 0;
        }

        private void Advance()
            => _currentPosition++;

        private bool TryEat(TokenTypes type, [NotNullWhen(true)] out Token? token)
        {
            if (Current != null && Current.Type == type)
            {
                token = Current;
                Advance();
                return true;
            }

            token = null;
            return false;
        }

        private Token Eat(TokenTypes type)
        {
            if (Current == null)
                throw new Exception($"Unexpected end of input. Expecting token of type '{type}'.");

            if (Current.Type != type)
                throw new Exception($"Unexpected token of type '{Current.Type}'. Expecting token of type '{type}' at position {Current.Start + 1}.");

            var result = Current;
            Advance();

            return result;
        }

        public Node Parse()
            => ParseOrExpression();

        private Node ParseOrExpression()
        {
            var node = ParseAndExpression();

            while (TryEat(TokenTypes.Or, out _))
            {
                var right = ParseAndExpression();
                node = new OrNode(node, right);
            }

            return node;
        }

        private Node ParseAndExpression()
        {
            var node = ParseNotExpression();

            while (TryEat(TokenTypes.And, out _))
            {
                var right = ParseNotExpression();
                node = new AndNode(node, right);
            }

            return node;
        }

        private Node ParseNotExpression()
        {
            if (TryEat(TokenTypes.Not, out _))
            {
                var right = ParseNotExpression();
                return new NotNode(right);
            }
            else
            {
                return ParsePrimary();
            }
        }

        /// <summary>
        /// Primary -> 'exists' Claim | Claim 'equals' String | Claim 'contains' String | Claim 'startsWith' String | Claim 'endsWith' String | '(' Expression ')'
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Node ParsePrimary()
        {
            Node node;

            if (TryEat(TokenTypes.Exists, out _))
            {
                node = new ExistsNode(Eat(TokenTypes.Claim));
            }
            else if (TryEat(TokenTypes.Claim, out var claimToken))
            {
                if (TryEat(TokenTypes.Equals, out var equalsToken))
                {
                    if (TryEat(TokenTypes.Integer, out var intToken))
                    {
                        node = new EqualsNode(claimToken, intToken);
                    }
                    else if (TryEat(TokenTypes.Float, out var floatToken))
                    {
                        node = new EqualsNode(claimToken, floatToken);
                    }
                    else if (TryEat(TokenTypes.String, out var stringToken))
                    {
                        node = new EqualsNode(claimToken, stringToken);
                    }
                    else
                    {
                        throw new Exception($"Only tokens of type '{TokenTypes.String}', '{TokenTypes.Integer}' or '{TokenTypes.Float}' are allowed after token of type '{TokenTypes.Equals}' at position {equalsToken.Start + equalsToken.Length + 1}.");
                    }
                }
                else if (TryEat(TokenTypes.Greater, out var greaterToken))
                {
                    if (TryEat(TokenTypes.Integer, out var intToken))
                    {
                        node = new GreaterNode(claimToken, intToken);
                    }
                    else if (TryEat(TokenTypes.Float, out var floatToken))
                    {
                        node = new GreaterNode(claimToken, floatToken);
                    }
                    else if (TryEat(TokenTypes.String, out var stringToken))
                    {
                        node = new GreaterNode(claimToken, stringToken);
                    }
                    else
                    {
                        throw new Exception($"Only tokens of type '{TokenTypes.String}', '{TokenTypes.Integer}' or '{TokenTypes.Float}' are allowed after token of type '{TokenTypes.Equals}' at position {greaterToken.Start + greaterToken.Length + 1}.");
                    }
                }
                else if (TryEat(TokenTypes.GreaterOrEqual, out var greaterOrEqual))
                {
                    if (TryEat(TokenTypes.Integer, out var intToken))
                    {
                        node = new GreaterOrEqualNode(claimToken, intToken);
                    }
                    else if (TryEat(TokenTypes.Float, out var floatToken))
                    {
                        node = new GreaterOrEqualNode(claimToken, floatToken);
                    }
                    else if (TryEat(TokenTypes.String, out var stringToken))
                    {
                        node = new GreaterOrEqualNode(claimToken, stringToken);
                    }
                    else
                    {
                        throw new Exception($"Only tokens of type '{TokenTypes.String}', '{TokenTypes.Integer}' or '{TokenTypes.Float}' are allowed after token of type '{TokenTypes.Equals}' at position {greaterOrEqual.Start + greaterOrEqual.Length + 1}.");
                    }
                }
                else if (TryEat(TokenTypes.Less, out var lessToken))
                {
                    if (TryEat(TokenTypes.Integer, out var intToken))
                    {
                        node = new LessNode(claimToken, intToken);
                    }
                    else if (TryEat(TokenTypes.Float, out var floatToken))
                    {
                        node = new LessNode(claimToken, floatToken);
                    }
                    else if (TryEat(TokenTypes.String, out var stringToken))
                    {
                        node = new LessNode(claimToken, stringToken);
                    }
                    else
                    {
                        throw new Exception($"Only tokens of type '{TokenTypes.String}', '{TokenTypes.Integer}' or '{TokenTypes.Float}' are allowed after token of type '{TokenTypes.Equals}' at position {lessToken.Start + lessToken.Length + 1}.");
                    }
                }
                else if (TryEat(TokenTypes.LessOrEqual, out var lessOrEqual))
                {
                    if (TryEat(TokenTypes.Integer, out var intToken))
                    {
                        node = new LessOrEqualNode(claimToken, intToken);
                    }
                    else if (TryEat(TokenTypes.Float, out var floatToken))
                    {
                        node = new LessOrEqualNode(claimToken, floatToken);
                    }
                    else if (TryEat(TokenTypes.String, out var stringToken))
                    {
                        node = new LessOrEqualNode(claimToken, stringToken);
                    }
                    else
                    {
                        throw new Exception($"Only tokens of type '{TokenTypes.String}', '{TokenTypes.Integer}' or '{TokenTypes.Float}' are allowed after token of type '{TokenTypes.Equals}' at position {lessOrEqual.Start + lessOrEqual.Length + 1}.");
                    }
                }
                else if (TryEat(TokenTypes.Contains, out _))
                {
                    node = new ContainsNode(claimToken, Eat(TokenTypes.String));
                }
                else if (TryEat(TokenTypes.StartsWith, out _))
                {
                    node = new StartsWithNode(claimToken, Eat(TokenTypes.String));
                }
                else if (TryEat(TokenTypes.EndsWith, out _))
                {
                    node = new EndsWithNode(claimToken, Eat(TokenTypes.String));
                }
                else
                {
                    throw new Exception($"Only tokens of type '{TokenTypes.Equals}', '{TokenTypes.Contains}', '{TokenTypes.StartsWith}' and '{TokenTypes.EndsWith}' are allowed after token of type '{claimToken.Type}' at position {claimToken.Start + claimToken.Length + 1}.");
                }
            }
            else if (TryEat(TokenTypes.LeftParenthesis, out _))
            {
                node = Parse();

                Eat(TokenTypes.RightParenthesis);

                // nach einem vollständigen Ausdruck dürfen nur 'and', 'or' oder Ende folgen
                if (Current != null && Current.Type != TokenTypes.And && Current.Type != TokenTypes.Or && Current.Type != TokenTypes.Not)
                    throw new Exception($"Unexpected token of type '{Current.Type}' at position {Current.Start + 1}.");
            }
            else if (Current == null)
            {
                throw new Exception($"Unexpected end of input.");
            }
            else
            {
                throw new Exception($"Unexpected token of type '{Current!.Type}' at position {Current!.Start + 1}.");
            }

            // Nach Primary darf nur 'and', 'or', ')' oder Ende erfolgen
            if (Current != null && Current.Type != TokenTypes.And && Current.Type != TokenTypes.Or && Current.Type != TokenTypes.RightParenthesis)
                throw new Exception($"Unexpected token of type '{Current.Type}' at position {Current.Start + 1}.");

            return node;
        }
    }
}
