--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0 (Debian 16.0-1.pgdg120+1)
-- Dumped by pg_dump version 16.0 (Debian 16.0-1.pgdg120+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: usrs; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.usrs (
    id integer NOT NULL,
    username character varying(50),
    email character varying(100)
);


ALTER TABLE public.usrs OWNER TO admin;

--
-- Name: usrs_id_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

CREATE SEQUENCE public.usrs_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.usrs_id_seq OWNER TO admin;

--
-- Name: usrs_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: admin
--

ALTER SEQUENCE public.usrs_id_seq OWNED BY public.usrs.id;


--
-- Name: usrs id; Type: DEFAULT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.usrs ALTER COLUMN id SET DEFAULT nextval('public.usrs_id_seq'::regclass);


--
-- Data for Name: usrs; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.usrs (id, username, email) FROM stdin;
1	user1	user1@example.com
2	user2	user2@example.com
\.


--
-- Name: usrs_id_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.usrs_id_seq', 2, true);


--
-- Name: usrs usrs_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.usrs
    ADD CONSTRAINT usrs_pkey PRIMARY KEY (id);


--
-- PostgreSQL database dump complete
--

