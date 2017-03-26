using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Practica1_0781.ParteA
{
    public class GrammarP1:Grammar
    {
        public GrammarP1():base(caseSensitive:true)
        {
            /*************** Comentarios ***************/
            //No hay :D


            /*************** Reservados ***************/            
            MarkReservedWords("CONJ");
            MarkReservedWords("RESERV");
            MarkReservedWords("retorno");
            MarkReservedWords("yytext");
            MarkReservedWords("yyline");
            MarkReservedWords("yyrow");
            MarkReservedWords("error");
            MarkReservedWords("int");
			MarkReservedWords("float");
			MarkReservedWords("string");
			MarkReservedWords("char");
			MarkReservedWords("bool");

            var conj = ToTerm("CONJ");
            var reserv = ToTerm("RESERV");
            var retorno = ToTerm("retorno");
            var yytext = ToTerm("yytext");
            var yyline = ToTerm("yyline");
            var yyrow = ToTerm("yyrow");
            var rerror = ToTerm("error");
            var rint = ToTerm("int");
            var rfloat = ToTerm("float");
            var rstring = ToTerm("string");
            var rchar = ToTerm("char");
            var rbool = ToTerm("bool");

            var flecha = ToTerm("->");
            var fin = ToTerm(";");
            var dosp = ToTerm(":");
            var coma = ToTerm(",");
            var apar = ToTerm("(");
            var cpar = ToTerm(")");
            var acor = ToTerm("[");
            var ccor = ToTerm("]");
            var and = ToTerm(".");
            var or = ToTerm("|");
            var muchos = ToTerm("+");
            var nada_muchos = ToTerm("*");
            var nada = ToTerm("?");

            //var barra = ToTerm("\\");
            var salto = ToTerm("\\n");
            var retorno_carro = ToTerm("\\r");
            var tab = ToTerm("\\t");
            var comillas_s = ToTerm("\\'");
            var comillas_d = ToTerm("\\\"");
            var blanco = ToTerm("[:blanco:]");
            var todo = ToTerm("[:todo:]");


            /*************** No Terminales ***************/
            var INICIO = new NonTerminal("INICIO", typeof(AstNode));
            var CUERPO = new NonTerminal("CUERPO", typeof(AstNode));
			var CODIGO = new NonTerminal("CODIGO", typeof(AstNode));
			var SENTENCIA = new NonTerminal("SENTENCIA", typeof(AstNode));
			var CONJUNTO = new NonTerminal("CONJUNTO", typeof(AstNode));
            var SERIE = new NonTerminal("SERIE", typeof(AstNode));
            var SERIE2 = new NonTerminal("SERIE2", typeof(AstNode));
            var LISTA = new NonTerminal("LISTA", typeof(AstNode));
            var SIMBOLO = new NonTerminal("SIMBOLO", typeof(AstNode));
			var REGLA = new NonTerminal("REGLA", typeof(AstNode));
            var METODO = new NonTerminal("METODO", typeof(AstNode));
            var ER = new NonTerminal("ER", typeof(AstNode));
            var ESCAPE = new NonTerminal("ESCAPE", typeof(AstNode));
            var M1 = new NonTerminal("M1", typeof(AstNode));
            var M2 = new NonTerminal("M2", typeof(AstNode));
            var TIPO = new NonTerminal("TIPO", typeof(AstNode));
            var RERROR = new NonTerminal("ERROR", typeof(AstNode));
            var RESERVAR = new NonTerminal("RESERVAR", typeof(AstNode));
            var RESERVADAS = new NonTerminal("RESERVADAS", typeof(AstNode));
            var RESERVADA = new NonTerminal("RESERVADA", typeof(AstNode));


            /*************** Terminales ***************/
            NumberLiteral numero = TerminalFactory.CreateCSharpNumber("numero");
            IdentifierTerminal id = TerminalFactory.CreateCSharpIdentifier("id");
            var tstring = new StringLiteral("tstring", "\"", StringOptions.AllowsDoubledQuote);
            var tchar = new StringLiteral("tchar", "'", StringOptions.AllowsDoubledQuote);            
            RegexBasedTerminal ascii = new RegexBasedTerminal("ascii", "[^\008\009\010\011\013\020\024\032\034\039\092\096\0239\0249]");

            //AstNode
            numero.AstConfig.NodeType = typeof(AstNode);
            id.AstConfig.NodeType = typeof(AstNode);
            tstring.AstConfig.NodeType = typeof(AstNode);
            tchar.AstConfig.NodeType = typeof(AstNode);            
            ascii.AstConfig.NodeType = typeof(AstNode);


            /*************** Gramatica ***************/
            this.Root = INICIO;

            INICIO.Rule = CUERPO;

            CUERPO.Rule = ToTerm("%%") + CODIGO + ToTerm("%%");

            CODIGO.Rule = MakeStarRule(CODIGO, SENTENCIA);

            SENTENCIA.Rule = CONJUNTO
                | REGLA
                | RERROR;

            CONJUNTO.Rule = conj + dosp + id + flecha + SERIE + fin
                | conj + dosp + id + flecha + LISTA + fin;

            //SERIE.Rule = SIMBOLO + ToTerm("~") + SIMBOLO;
            //SERIE.Rule = SERIE2;
            SERIE.Rule = SIMBOLO + ToTerm("~") + SIMBOLO;

            LISTA.Rule = MakeStarRule(LISTA, coma, SIMBOLO);

            SIMBOLO.Rule = SERIE
                | ESCAPE
                | numero
                | tstring
                | tchar
                | id
                | ascii;

            REGLA.Rule = id + flecha + ER + flecha + METODO + fin
                | id + flecha + ER + flecha + METODO + flecha + RESERVAR + fin
                | id + flecha + ER + flecha + METODO + fin + flecha + RESERVAR + fin;

            ER.Rule = ER + ER + and
                | ER + ER + or
                | ER + muchos
                | ER + nada_muchos
                | ER + nada
                | ESCAPE
                | tstring
                | tchar
                | numero
                | id;

            ESCAPE.Rule = salto
                | retorno_carro
                | tab
                | comillas_s
                | comillas_d
                | blanco
                | todo;

            /*ESCAPE.Rule = barra+ToTerm("n")
                | barra + ToTerm("r")
                | barra + ToTerm("t")
                | barra + ToTerm("'")
                | barra + ToTerm("\"")
                | blanco
                | todo;*/

            METODO.Rule = M1
                | M2;

            RESERVAR.Rule = reserv + acor + RESERVADAS + ccor;

            RESERVADAS.Rule = MakeStarRule(RESERVADAS, RESERVADA);

            RESERVADA.Rule = tstring + flecha + retorno + apar + id + coma + yyline + coma + yyrow + cpar + fin
                | id + flecha + retorno + apar + id + coma + yyline + coma + yyrow + cpar + fin;

            M1.Rule = retorno + apar + id + coma + yyline + coma + yyrow + cpar;

            M2.Rule = retorno + apar + id + coma + yytext + coma + TIPO + coma + yyline + coma + yyrow + cpar;

            TIPO.Rule = rint
                | rfloat
                | rstring
                | rchar
                | rbool;

            RERROR.Rule = rerror + flecha + ER + flecha + rerror + apar + yyline + coma + yyrow + coma + yytext + cpar + fin
                | id + flecha + ER + flecha + rerror + apar + yyline + coma + yyrow + coma + yytext + cpar + fin;


            /*************** Precedencia y Asociatividad ***************/
            //RegisterOperators(1, or, and);
            //RegisterOperators(2, muchos, nada_muchos, nada);

            /*************** Errores ***************/
            CODIGO.ErrorRule = SyntaxError + CODIGO;
            LISTA.ErrorRule = SyntaxError + LISTA;
            RESERVADAS.ErrorRule = SyntaxError + RESERVADAS;


            /*************** Eliminación ***************/
            this.MarkPunctuation("%%", "->", ":", ";", ",", "~", "(", ")", "[", "]", "CONJ", "RESERV", "retorno");
            this.MarkTransient(CUERPO, SENTENCIA, SIMBOLO, RESERVAR, METODO, TIPO);

            LanguageFlags = LanguageFlags.CreateAst;
        }        
    }
}


