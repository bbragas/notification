﻿using MediatR;

namespace Notification.Api.Queries.QueryHandlers.v1.Whatsapp.GetWhatsappNotSentQueueCountQuery;

public record GetNotSentQueueCountQueryRequest : IRequest<GetNotSentQueueCountQueryResponse>;
