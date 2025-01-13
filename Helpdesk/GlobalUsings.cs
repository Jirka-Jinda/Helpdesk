// Standard Libraries
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Caching.Memory;
global using System.Text.Json;

// Project dependencies
global using Domain.Ticket;

// Internal dependencies
global using Helpdesk.Filters.Global;
global using Helpdesk.Models.Navigation;
global using Helpdesk.Models.Storage;
global using Helpdesk.Models.Storage.Manager;
global using Helpdesk.Models.Cache.SessionCache;
global using Helpdesk.Models.Cache.ScopeCache;
global using Helpdesk.Models.Settings;
global using Helpdesk.Models.Settings.Enums;
global using Helpdesk.Models.EventPlanner;
global using Helpdesk.Models.EventPlanner.Enums;
global using Helpdesk.Models.EventPlanner.Planner;
global using Helpdesk.Models.EventPlanner.Manager;
global using Helpdesk.Models.Attributes;