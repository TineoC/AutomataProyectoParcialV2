grammar AUTOMATA;

automata: state+ EOF;

state: ( id GREATER_THAN transitions | id) SEMI;

transitions: transition+;

id: prop state_name;

state_name: TEXT;

prop: initial? acceptance?;

initial: HASH_TAG;

acceptance: ASTERISK;

transition:
	input SEPARATOR (
		OPEN_BRACKET state_name (COMMA state_name)+ CLOSE_BRACKET
		| state_name
	) COMMA*;

input: TEXT;

SEPARATOR: ':';
HASH_TAG: '#';
ASTERISK: '*';
GREATER_THAN: '>';
TEXT: [a-zA-Z0-9]+;
COMMA: ',';
SEMI: ';';

OPEN_BRACKET: '[';
CLOSE_BRACKET: ']';

IGNORE: [ \n\r\t] -> skip;